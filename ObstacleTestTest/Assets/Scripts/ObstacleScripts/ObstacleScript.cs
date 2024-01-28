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
    private AnimationCurve gravityCurve;
    [SerializeField]
    private AnimationCurve bounceCurve;

    
    //Variables
    public ObstacleColor obstacleColor;         //ObstacleClasses from ObstacleManager
    public ObstacleSize obstacleSize; 
                                
    //Movement Variables
    private Vector3 startPosition;
    private Vector3 finalPosition;
    private Vector3 currentPosition;
    private float timeSinceBounce;
    private float bouncePercent;
    private float bounceHeight;


    void Start()
    {
        //Set References
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        //Set Obstacle classes variables
        if(obstacleColor != null){
            
            //Debug.Log("obstcleColor.bounces = " + obstacleColor.bounces);
            spriteRenderer.color = obstacleColor.color;
        }
        else{
            //Debug.LogError("obstacle is without obstacleColor!");
        }

        if(obstacleSize != null)
        {
            //Debug.Log("obstacleSize.scale = " + obstacleSize.scale);
            transform.localScale = new Vector3 (obstacleSize.scale, obstacleSize.scale, transform.localScale.z);
        }
        else{
            //Debug.LogError("obstacle is without obstacleSize!");
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
        }
        else if( transform.position.x < -9)
        {
            finalPosition.x = startPosition.x + (21 * (bounceHeight/9));
        }

       //Debug.Log("finalPosition = " + finalPosition + " and startPosition = " + startPosition + " and bounceHeight = " + bounceHeight);
    }

    void Update(){
        //Calculate the time between bounces and 
        timeSinceBounce += Time.deltaTime;
        float totalBounceTime = Mathf.Sqrt((2*bounceHeight)/(10)) * obstacleSize.bounceTimeModifier;    //From time = square root (2*height/gravity)
        bouncePercent = timeSinceBounce / totalBounceTime;

        if(bouncePercent <=1)       //Decide bounce percent complete
        {
            float gravityMultiplier = gravityCurve.Evaluate(bouncePercent);
            currentPosition.y = Mathf.Lerp(startPosition.y, finalPosition.y, gravityMultiplier);

            float xMultiplier = xCurve.Evaluate(bouncePercent);
            currentPosition.x = Mathf.Lerp(startPosition.x, finalPosition.x, xMultiplier);

            transform.position = currentPosition;
        }
        else
        {
            if(obstacleColor.bounces >=1)
            {
                //Bounce
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
