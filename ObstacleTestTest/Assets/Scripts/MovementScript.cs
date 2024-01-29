using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    //References
    public Rigidbody2D rb;

    //variables
    public bool isMovingLeft = false;
    public bool isMovingRight = false;
    public float movementSpeed;


    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown("a")) {
           isMovingRight = false;
           isMovingLeft = true;
        }
        
        if(Input.GetKeyUp("a")) {
            CheckIfMoving();
        }

        if(Input.GetKeyDown("d")) {
            isMovingLeft = false;
            isMovingRight = true;
        }
        
        if(Input.GetKeyUp("d")) {
            CheckIfMoving();
        }
        

        if(isMovingLeft)
        {
            rb.velocity = new Vector2(-movementSpeed, 0);
        }

        if(isMovingRight)
        {
            rb.velocity = new Vector2(movementSpeed, 0);
        }

        if(!isMovingLeft && !isMovingRight)
        {
           StandStill();
        }

    }

    private void CheckIfMoving()
    {
        if(isMovingLeft && isMovingRight)
        {
            StandStill();
        }
        else if(Input.GetKey("a"))
        {
            isMovingRight = false;
            isMovingLeft = true;
            Debug.Log("checking if standing still and moving left.");
        }
        else if(Input.GetKey("d"))
        {
            isMovingLeft = false;
            isMovingRight = true;
            Debug.Log("checking if standing still and moving right.");
        }
        else 
        {
            StandStill();
        }
    }

    private void StandStill()
    {
        isMovingLeft = false;
        isMovingRight = false;
        rb.velocity = new Vector2 (0,0);
    }
}
