using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    //References
    private SpriteRenderer spriteRenderer;
    
    //Variables
    public ObstacleColor obstacleColor;         //ColorClass
    public ObstacleSize obstacleSize; 
    
    [SerializeField]
    private float movementFloat;
    private Vector3 movement;
    



    // Start is called before the first frame update
    void Start()
    {
        
        movementFloat = 0.01f;          //Set movement variables    OBSOBS!! KOLLA HÄR PAPPA!
        movement = new Vector3(0, -movementFloat, 0);
        
        //Set variables
        spriteRenderer = GetComponent<SpriteRenderer>();
        
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
    }

    void Update(){
        transform.position += movement;     //OBSOBS HÄR SKER SJÄLVA RÖRELSEN PAPPA

    }
}
