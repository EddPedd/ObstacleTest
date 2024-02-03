using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    //References
    public Rigidbody2D rb;

    //variables
    private bool isMovingLeft = false;
    private bool isMovingRight = false;
    [SerializeField]
    private float movementSpeed;

    private float direction;

    //Skip variables
    public bool canSkip = false;
    private bool isSkipping = false;
    private float currentSkippingTime;
    [SerializeField]
    private float skipSpeed;
    [SerializeField]
    private float skipTime;

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
        
if(Input.GetKeyDown(KeyCode.Space) && canSkip && (isMovingLeft || isMovingRight)) 
        {
            canSkip = false;
            CheckDirection();
            isMovingLeft = false;
            isMovingRight = false;
            currentSkippingTime = 0;
            isSkipping = true;
        }

        if(isMovingLeft)
        {
            rb.velocity = new Vector2(-movementSpeed, 0);
        }

        if(isMovingRight)
        {
            rb.velocity = new Vector2(movementSpeed, 0);
        }

        if(isSkipping)
        {
            currentSkippingTime += Time.deltaTime;
            if(currentSkippingTime < skipTime)
            {
                rb.velocity = new Vector2(skipSpeed*direction,0);
            }
            else
            {
                isSkipping = false;
                CheckIfMoving();
            }
            
        }
        
        if(!isMovingLeft && !isMovingRight && !isSkipping)
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

    private void CheckDirection()
    {
        if(isMovingLeft)
        {
            direction= (-1f);
        }
        else
        {
            direction = 1;
        }
    }

    private void StandStill()
    {
        isMovingLeft = false;
        isMovingRight = false;
        rb.velocity = new Vector2 (0,0);
    }
}
