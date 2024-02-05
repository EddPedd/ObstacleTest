using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    //References
    private SpriteRenderer spriteRenderer;
    private Transform floor;
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
    private Vector3 startPosition;
    private Vector3 finalPosition;
    private Vector3 currentPosition;
    private float timeSinceBounce;
    private float bouncePercent;
    private float bounceHeight;
    private int bounces;
    private float xDifference;
    private float maxHeight = 3;
    private float totalBounceTime;

    void Start()
    {
        //Set References
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        //Set Obstacle classes variables
        if(obstacleColor != null){
            
            //Debug.Log("obstcleColor.bounces = " + obstacleColor.bounces);
            spriteRenderer.color = obstacleColor.color;
            bounces = obstacleColor.bounces;
        }
        else{
            Debug.LogError("obstacle is without obstacleColor!");
        }

        if(obstacleSize != null)
        {
            //Debug.Log("obstacleSize.scale = " + obstacleSize.scale);
            transform.localScale = new Vector3 (obstacleSize.scale, obstacleSize.scale, transform.localScale.z);
        }
        else{
            Debug.LogError("obstacle is without obstacleSize!");
        }

        if(obstacleShape != null)
        {
            spriteRenderer.sprite = obstacleShape.shapeSprite;
            obstacleShape.AddCollisionCollider(gameObject);
        }

        //set movement variables
        startPosition = transform.position;
        
        GameObject floorObject = GameObject.FindWithTag("Floor");
        floor = floorObject.GetComponent<Transform>();

        if(floor != null)       //Calculate where obstacle and floor meet and the height between the obstacle and the floor
        {            
            finalPosition.y  = floor.position.y + (floor.localScale.y/2) + obstacleSize.scale/2;
            bounceHeight = startPosition.y - finalPosition.y;                           
        }
        else if (floor = null)
        {
            Debug.LogWarning("Can't define obstacleObjects finalPosition because floor Transform is missing!");
        }

        if (transform.position.x > 9)       //9 for the x-value of right spawnPoint object
        {
            finalPosition.x = startPosition.x - (21 * (bounceHeight/9));      //9 for the height of the spawnPoint relative to the floor (the max height possible), 21 for the total lenght of th evisable floor.
            finalPosition.x = Mathf.Clamp(finalPosition.x, 0f, 7f);
        }
        else if( transform.position.x < -9)
        {
            finalPosition.x = startPosition.x + (21 * (bounceHeight/9));
            finalPosition.x = Mathf.Clamp(finalPosition.x, -7f, 0f);
        }
        else
        {
            finalPosition.x = startPosition.x;
        }

        xDifference = startPosition.x - finalPosition.x;
       //Debug.Log("finalPosition = " + finalPosition + " and startPosition = " + startPosition + " and bounceHeight = " + bounceHeight);

        totalBounceTime = Mathf.Sqrt((2*bounceHeight)/(10)) * obstacleSize.bounceTimeModifier;    //From time = square root (2*height/gravity)
    }

    void Update(){
        //Calculate the time between bounces and 
        timeSinceBounce += Time.deltaTime;
        
        bouncePercent = timeSinceBounce / totalBounceTime;

        if(bouncePercent <0.5 )
        {
            float risePercent = bouncePercent*2;
            float gravityMultiplier = riseCurve.Evaluate(risePercent);
            currentPosition.y = Mathf.Lerp(startPosition.y, maxHeight, gravityMultiplier);
        }
        else if (bouncePercent >= 0.5f && 1f > bouncePercent )
        {
            float fallPercent = 2*(bouncePercent-0.5f);
            float gravityMultiplier = fallCurve.Evaluate(fallPercent);
            currentPosition.y = Mathf.Lerp(maxHeight, finalPosition.y, gravityMultiplier);
        }
        
        if(bouncePercent < 1)       //Decide bounce percent complete
        {
            float xMultiplier = xCurve.Evaluate(bouncePercent);
            currentPosition.x = Mathf.Lerp(startPosition.x, finalPosition.x, xMultiplier);

            transform.position = currentPosition;
        }
        else
        {
            if(bounces >=1)
            {
                Transform player = GameObject.FindWithTag("Player").GetComponent<Transform>();
                
                float directionMultiplier = -1f;

                if(transform.position.x < player.position.x)
                {
                    directionMultiplier = 1f;
                }

                Debug.Log("directionMultiplier before Calculated bounce = " + directionMultiplier);

                obstacleShape.CalculateBounce(directionMultiplier);

                Debug.Log("final directionMultiplier = " + directionMultiplier );
                
                //reset positions for bounce-calculations
                startPosition = transform.position;
                maxHeight -= 2.5f;
                finalPosition.x += xDifference*directionMultiplier/2;

                bounces -= 1 ;
                timeSinceBounce = 0;
                return;//Bounce
            }
            else
            {
                //Pop
                GameObject.Destroy(gameObject);
                return;
            }
        }
    }

    void OnTriggerEnter2D (Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
            HealthManager.Instance.ChangeHealth(-1);
            Debug.Log(gameObject.name + " collided with Player");
        }
    }
}
