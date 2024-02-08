using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointBallScript : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve fallCurve;
    
    [SerializeField]
    private float lifeTime;
    private float life = 0;
    
    private Vector3 currentPosition;

    [SerializeField]
    private float fallTime;
    private float fallPercent;

    private Vector3 startPosition;
    private Vector3 endPosition;
    
    void Start()
    {
        startPosition = transform.position;
        endPosition = new Vector3(transform.position.x, (transform.localScale.x/2f)-4.5f, transform.position.z);    //4.5 from half the scale of the floor but I cant be bothered to reference it so I just write it out
    }

    void Update()
    {
        life += Time.deltaTime;
        
        if(life >= lifeTime) 
        {
            GameObject.Destroy(gameObject);
        }  
        
        fallPercent = life/fallTime;

        float fallMultiplier = fallCurve.Evaluate(fallPercent);
        currentPosition.y = Mathf.Lerp(startPosition.y, endPosition.y, fallMultiplier);
        
        transform.position = currentPosition;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
            PointManagerScript.Instance.UpdatePoints(100);
            GameObject.Destroy(gameObject);
            Debug.Log("PointBall was picked up and sent message to PointManager to UpdatePoints");
        }
    }
}
