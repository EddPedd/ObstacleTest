using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointball2Script : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve riseCurve;
    [SerializeField]
    private AnimationCurve fallCurve;
    [SerializeField]
    private AnimationCurve xCurve;

    private Vector3 startPosition;
    private Vector3 endPosition;
    private Vector3 currentPosition;
    [SerializeField]
    private float maxHeight; 

    private float timeSinceSpawn;
    [SerializeField]
    private float fallTime;
    private float bouncePercent;

    [SerializeField]
    private float lifeTime;

    void Update()
    {
        timeSinceSpawn += Time.deltaTime;
        bouncePercent = timeSinceSpawn/fallTime;

        if(timeSinceSpawn < fallTime/2) 
        {
            float risePercent = bouncePercent*2;
            float riseMultiplier = riseCurve.Evaluate(risePercent);
            currentPosition.y = Mathf.Lerp(startPosition.y, maxHeight, riseMultiplier);
        }
        else if(timeSinceSpawn >= fallTime/2 && timeSinceSpawn <= fallTime)
        {
            float fallPercent = 2*(bouncePercent-0.5f);
            float fallMultiplier = fallCurve.Evaluate(fallPercent);
            currentPosition.y = Mathf.Lerp( maxHeight, endPosition.y, fallMultiplier);
        }

        if(timeSinceSpawn <= 1)
        {
            float xMultiplier = xCurve.Evaluate(bouncePercent);
            currentPosition.x = Mathf.Lerp(startPosition.x, endPosition.x, xMultiplier);

            transform.position = currentPosition;
        }
    }

    public void SetEndPosition(Vector3 _startPosition, float _offSet)
    {
        startPosition = _startPosition;
        currentPosition = _startPosition;

        float offset = 6*_offSet;
        float finalXPosition = _startPosition.x + offset - 3;
        float finalYPosition = (-4.5f + transform.localScale.y/2);     //-4,5 for top of floor

        endPosition = new   Vector3(finalXPosition, finalYPosition, _startPosition.z);  
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        //Add behaviour for trigger with player
    }
}
