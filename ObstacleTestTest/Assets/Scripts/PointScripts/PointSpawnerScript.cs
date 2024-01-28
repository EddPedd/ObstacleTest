using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSpawnerScript : MonoBehaviour
{
    [SerializeField]
    private Transform topSpawn;

    [SerializeField]
    private GameObject pointBallPrefab;

    [SerializeField]
    private float timeBeteweenSpawns;
    private float timesSinceLastSpawn;

    void Update()
    {
        timesSinceLastSpawn += Time.deltaTime;

        if(timesSinceLastSpawn >= timeBeteweenSpawns)
        {
            SpawnRandomPointBall();  
            timesSinceLastSpawn = 0;  
        }
    }

    void SpawnRandomPointBall()
    {
        float offSet = Random.Range(-10f, 11f);
        float xOffSet = topSpawn.position.x + offSet;

        Vector3 spawn = new Vector3(xOffSet, topSpawn.position.y, topSpawn.position.z);
        
        GameObject pointBall = Instantiate(pointBallPrefab, spawn, topSpawn.rotation);

        Debug.Log("offSet is equal to " + offSet +" and xOffSet is equal to " + xOffSet + " and spawn.x = " + spawn.x);
    
    }
}
