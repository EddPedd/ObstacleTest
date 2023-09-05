using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    //Variables
    public ObstacleColor obstacleColor;         //ColorClass
    
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private float movementFloat;
    private Vector3 movement;
    // Start is called before the first frame update
    void Start()
    {
        //Set movement variables    OBSOBS!! KOLLA HÄR PAPPA!
        movementFloat = 0.01f;
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
    }

    void Update(){
        transform.position += movement;     //OBSOBS HÄR SKER SJÄLVA RÖRELSEN PAPPA

    }
}
