using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    //References
    private SpriteRenderer spriteRenderer;
    private Transform floor;
    [SerializeField]
    public Transform player {get; private set;} 
    [SerializeField]
    private AnimationCurve xCurve;
    [SerializeField]
    private AnimationCurve riseCurve;
    [SerializeField]
    private AnimationCurve fallCurve;

    //Variables
    public ObstacleColor obstacleColor;         //ObstacleClasses from ObstacleManager
    public ObstacleSize obstacleSize;
    public ObstacleShape obstacleShape; 
                                
    //Movement Variables
    [SerializeField]
    public Vector3 startPosition {get; private set;} //Used to calculate bounce direction for circles
    private Vector3 startBouncePosition;
    private Vector3 finalPosition;
    private Vector3 currentPosition;
    private float timeSinceBounce;
    private float bouncePercent;
    private int bounces;
    public float xDifference;
    private float maxHeight = 3;
    private float totalBounceTime;
    private bool spawnedFromSide = true;
    private bool hasBounced;
    [SerializeField]
    public float directionMultiplier;
    [SerializeField]
    public float scaleMultiplier;

    void Start()
    {
        //Set references
        spriteRenderer = GetComponent<SpriteRenderer>();
        startPosition = transform.position;
        startBouncePosition = startPosition;

        GameObject floorObject = GameObject.FindWithTag("Floor");
        floor = floorObject.GetComponent<Transform>();

        GameObject playerObject = GameObject.FindWithTag("Player");
        player = playerObject.GetComponent<Transform>();

        
        //Set Obstacle classes variables
        if(obstacleColor != null){      //Set Color
            
            //Debug.Log("obstcleColor.bounces = " + obstacleColor.bounces);
            spriteRenderer.color = obstacleColor.color;
            bounces = obstacleColor.bounces;
        }else{ Debug.LogError("obstacle is without obstacleColor!"); }

        if(obstacleSize != null)    //Set size
        {
            //Debug.Log("obstacleSize.scale = " + obstacleSize.scale);
            transform.localScale = new Vector3 (obstacleSize.scale, obstacleSize.scale, transform.localScale.z);
        } else{ Debug.LogError("obstacle is without obstacleSize!");}

        if(obstacleShape != null)   //Set shape
        {
            spriteRenderer.sprite = obstacleShape.shapeSprite;
            obstacleShape.AddCollisionCollider(gameObject);
            scaleMultiplier = obstacleShape.scaleMultiplier;
        }else{ Debug.LogError("obstacle is without obstacleShape!");}

        //Calculate finalPosition
        if(floor != null)       //Calculate where obstacle and floor meet and the height between the obstacle and the floor
        {            
            finalPosition.y  = floor.position.y + (floor.localScale.y/2) + (obstacleSize.scale*scaleMultiplier)/2;      
            Debug.Log("finalPosition.y = " + finalPosition.y + " and scaleMultiplier*obstacleSize.scale = " + scaleMultiplier*obstacleSize.scale + 
            " and floor.position.y + floor.localScale.y/2 = " + (floor.position.y + (floor.localScale.y/2)));                  
        }
        else if (floor = null){Debug.LogWarning("Can't define obstacleObjects finalPosition because floor Transform is missing!");}

        //Check where the obstacle is spawned from and decide how to move in x
        if (transform.position.x > 9)       //9 for the x-value of right spawnPoint object
        {
            finalPosition.x = Random.Range(0, 7);  
        }
        else if( transform.position.x < -9)
        {
            finalPosition.x = Random.Range(-7, 0);
        }
        else
        {
            finalPosition.x = startPosition.x;
            spawnedFromSide = false;
        }

        //Decide dependent variables
        xDifference = startPosition.x - finalPosition.x;
        totalBounceTime = obstacleSize.bounceTimeModifier;    //From time = square root (2*height/gravity)
        
        Debug.Log("finalPosition = " + finalPosition + " and startPosition = " + startPosition);
    }

    void Update(){
        //Bounce-math
        timeSinceBounce += Time.deltaTime;
        bouncePercent = timeSinceBounce / totalBounceTime;

        //Decide how to move in y during bounce
        if(bouncePercent <0.5 && spawnedFromSide) //Upward rise when spawned from side
        {
            float risePercent = bouncePercent*2;
            float gravityMultiplier = riseCurve.Evaluate(risePercent);
            currentPosition.y = Mathf.Lerp(startBouncePosition.y, maxHeight, gravityMultiplier);
        }
        else if (bouncePercent >= 0.5f && 1f > bouncePercent && spawnedFromSide)   //Downward fall after rise if spawned from side
        {
            float fallPercent = 2*(bouncePercent-0.5f);
            float gravityMultiplier = fallCurve.Evaluate(fallPercent);
            currentPosition.y = Mathf.Lerp(maxHeight, finalPosition.y, gravityMultiplier);
        }

        else if(bouncePercent < 1 && !spawnedFromSide && !hasBounced)  //y-movement if spawned from above and has not bounced yet
        {
            float gravityMultiplier = fallCurve.Evaluate(bouncePercent);
            currentPosition.y = Mathf.Lerp(startBouncePosition.y, finalPosition.y, gravityMultiplier);
        }

        else if(bouncePercent < 0.5f && !spawnedFromSide && hasBounced)  //y-movement if spawned from above and has not bounced yet
        {
            float risePercent = bouncePercent*2;
            float gravityMultiplier = riseCurve.Evaluate(risePercent);
            currentPosition.y = Mathf.Lerp(startBouncePosition.y, maxHeight, gravityMultiplier);
        }
        else if(bouncePercent > 0.5f && bouncePercent < 1 && !spawnedFromSide && hasBounced)  //y-movement if spawned from above and has not bounced yet
        {
            float fallPercent = 2*(bouncePercent-0.5f);
            float gravityMultiplier = fallCurve.Evaluate(fallPercent);
            currentPosition.y = Mathf.Lerp(maxHeight, finalPosition.y, gravityMultiplier);
        }
        
        //decide how to move in x during bounce
        if(bouncePercent < 1)       //Decide bounce percent complete
        {
            float xMultiplier = xCurve.Evaluate(bouncePercent);
            currentPosition.x = Mathf.Lerp(startBouncePosition.x, finalPosition.x, xMultiplier);

            //Update position
            transform.position = currentPosition;
        }

        //What to do when the bounce is complete
        else
        {
            if(bounces >=1) //What to do if bounce again
            {
                directionMultiplier = -1f;

                if(transform.position.x < player.position.x)
                {
                    directionMultiplier = 1f;
                }

                Debug.Log("directionMultiplier before Calculated bounce = " + directionMultiplier);

                //Decide x-direction depending on shape
                obstacleShape.CalculateBounce(directionMultiplier, this);

                Debug.Log("final directionMultiplier = " + directionMultiplier );
                
                //Decide new positions for bounce-calculations
                startBouncePosition = transform.position;
                maxHeight = obstacleSize.bounceHeight;
                finalPosition.x += directionMultiplier*Mathf.Abs(xDifference)/1.5f;

                bounces -= 1;
                hasBounced = true;

                //Reset bounce
                timeSinceBounce = 0;
                return;
            }
            else    //What to do if no more bounce
            {
                //Pop
                GameObject.Destroy(gameObject);
                return;
            }
        }
    }

    
    void OnTriggerEnter2D (Collider2D collider)
    {
        //Collision with player
        if(collider.CompareTag("Player"))
        {
            HealthManager.Instance.ChangeHealth(-1);
            Debug.Log(gameObject.name + " collided with Player");
        }
    }
}
