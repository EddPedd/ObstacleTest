using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    //Variables
    public ObstacleColor obstacleColor;         //ColorClass
    
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
