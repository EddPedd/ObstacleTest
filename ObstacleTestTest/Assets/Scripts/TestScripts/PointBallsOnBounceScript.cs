
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointBallsOnBounceScript : MonoBehaviour
{
    [SerializeField]
    private GameObject pointBallPrefab;

    [SerializeField]
    private int minBallsAmount;
    [SerializeField]
    private int maxBallsAmount;


    private void SpawnPointBalls()
    {
        int amountOfBalls = Random.Range(minBallsAmount, maxBallsAmount);

        for (int i = 0; i< amountOfBalls; i++)
        {
            GameObject pointBall = Instantiate(pointBallPrefab, transform.position, transform.rotation);

            float endPositionOffset = i/amountOfBalls;

            Pointball2Script ballScript = pointBall.GetComponent<Pointball2Script>();
            ballScript.SetEndPosition(transform.position, endPositionOffset);
        }
    }
}
