using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSpawnerScript : MonoBehaviour
{
    [SerializeField]
    private Transform topSpawn;

    public bool gameHasStarted;

    [SerializeField]
    private GameObject pointBallPrefab;
    [SerializeField]
    private float timeBeteweenPointSpawns;
    private float timeSinceLastPoint;

    [SerializeField]
    private GameObject multiplierCubePrefab;
    [SerializeField]
    private float timeBeteweenMultiplierSpawns;
    private float timeSincelastMultiplier;

    void Update()
    {
        if(gameHasStarted)
        {
            timeSinceLastPoint += Time.deltaTime;
            timeSincelastMultiplier += Time.deltaTime;
        } 

        if(timeSinceLastPoint >= timeBeteweenPointSpawns)
        {
            SpawnRandomPointBall();  
            timeSinceLastPoint = 0;  
        }

        if(timeSincelastMultiplier >= timeBeteweenMultiplierSpawns)
        {
            SpawnRandomMultiplierCube();  
            timeSincelastMultiplier = 0;  
        }
    }

    void SpawnRandomPointBall()
    {
        float offSet = Random.Range(0, 15);
        float xOffSet = topSpawn.position.x + offSet;

        Vector3 ballSpawn= new Vector3(xOffSet, topSpawn.position.y, topSpawn.position.z);
        
        Instantiate(pointBallPrefab, ballSpawn, topSpawn.rotation);

        Debug.Log("offSet for PointBall is equal to " + offSet +" and xOffSet is equal to " + xOffSet + " and spawn.x = " + ballSpawn.x);
    
    }

    void SpawnRandomMultiplierCube()
    {
        float offSet = Random.Range(0, 15);
        float xOffSet = topSpawn.position.x + offSet;

        Vector3 cubeSpawn = new Vector3(xOffSet, topSpawn.position.y, topSpawn.position.z);
        
        GameObject multiplierCube = Instantiate(multiplierCubePrefab, cubeSpawn, topSpawn.rotation);

        Debug.Log("offSet for MultiplierCube is equal to " + offSet +" and xOffSet is equal to " + xOffSet + " and spawn.x = " + cubeSpawn.x);
    
    }
    
    public void StartGame()
    {
        gameHasStarted = true;
    }
}
