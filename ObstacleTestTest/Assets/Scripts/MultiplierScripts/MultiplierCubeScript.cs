using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplierCubeScript : MonoBehaviour
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
    
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        endPosition = new Vector3(transform.position.x, (transform.localScale.x/2f)-4.5f, transform.position.z);
    }

    // Update is called once per frame
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
            PointManagerScript.Instance.UpdateMultiplier(1);
            GameObject.Destroy(gameObject);
            Debug.Log("MultiplierCube was picked up and sent message to PointManager to UpdateMultiplier");
        }
    }
}
