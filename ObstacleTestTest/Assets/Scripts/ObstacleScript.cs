using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    //References
    private SpriteRenderer spriteRenderer;
    private Transform floor;
    
    //Variables
    public ObstacleColor obstacleColor;         //ColorClass
    public ObstacleSize obstacleSize; 
    
    [SerializeField]
    private float movementFloat;
    private Vector3 movement;

    private Vector3 floorPosition;
    private Vector3 startPosition;
    private float timeSinceBounce;
    private float bouncePercent;
    private float bounceHeight;


    // Start is called before the first frame update
    void Start()
    {
       // movementFloat = 0.01f;          //Set movement variables    OBSOBS!! KOLLA HÄR PAPPA!
        //movement = new Vector3(0, -movementFloat, 0);
        
        //Set References
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        //Set Obstacle classes variables
        if(obstacleColor != null){
            
            Debug.Log("obstcleColor.bounces = " + obstacleColor.bounces);
            spriteRenderer.color = obstacleColor.color;
        }
        else{
            Debug.LogError("obstacle is without obstacleColor!");
        }

        if(obstacleSize != null)
        {
            Debug.Log("obstacleSize.scale = " + obstacleSize.scale);
            transform.localScale = new Vector3 (obstacleSize.scale, obstacleSize.scale, transform.localScale.z);
        }
        else{
            Debug.LogError("obstacle is without obstacleSize!");
        }

        //set movement variables
        startPosition = transform.position;
        
        GameObject floorObject = GameObject.FindWithTag("Floor");
        floor = floorObject.GetComponent<Transform>();
        if(floor != null)
        {            
            floorPosition = new Vector3 (transform.position.x ,floor.position.y + (floor.localScale.y/2) + obstacleSize.scale/2, transform.position.z);
            bounceHeight = startPosition.y - floorPosition.y;                           
        }
        else if (floor = null)
        {
            Debug.LogError("Can't define obstacleObjects floorPosition because floor Transform is missing!");
        }

        Debug.Log("startPosition = " + startPosition + ", floorPosition = " + floorPosition + " and bounceHeight = " + bounceHeight);
        Debug.Log(obstacleSize.gravityCurve);
    }

    void Update(){
        //Calculate the time between bounces and 
        timeSinceBounce += Time.deltaTime;
        float totalBounceTime = Mathf.Sqrt((2*bounceHeight)/(10)) * obstacleSize.bounceTimeModifier;    //From time = square root (2*height/gravity)
        bouncePercent = timeSinceBounce / totalBounceTime;

        if(bouncePercent <=1)       //Decide 
        {
            float gravityMultiplier = obstacleSize.gravityCurve.Evaluate(bouncePercent);
            transform.position = Vector3.Lerp(startPosition, floorPosition, gravityMultiplier);
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
           
       //transform.position += movement;     //OBSOBS HÄR SKER SJÄLVA RÖRELSEN PAPPA
    }
}
