using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSpawnerScript : MonoBehaviour
{
    [SerializeField]
    private Transform topSpawn;
    
    [SerializeField]
    private GameObject pointBallPrefab;
    public float timeBetweenPointBalls;
    private float timeSincePointBall;

    [SerializeField]
    private GameObject multiplierCubePrefab;
    public float timeBetweenMultiplierCube;
    private float timeSinceMultiplierCube;

    // Update is called once per frame
    void Update()
    {
        timeSincePointBall += Time.deltaTime;
        timeSinceMultiplierCube += Time.deltaTime;

        if(timeSinceMultiplierCube >= timeBetweenMultiplierCube)
        {
            timeSinceMultiplierCube = 0;
            SpawnMultiplierCube();
            return;
        }
        
        if(timeSincePointBall >= timeBetweenPointBalls)
        {
            timeSincePointBall = 0;
            SpawnPointBall();
            return;
        }
    }

    void SpawnMultiplierCube()
    {
        float offSet = Random.Range(0f, 14f);
        Vector3 finalSpawn = new Vector3(topSpawn.position.x+offSet, topSpawn.position.y, topSpawn.position.z);
        GameObject cube = Instantiate(multiplierCubePrefab, finalSpawn, topSpawn.rotation);
        Debug.Log("offSet = " + offSet + " and finalSpawn = " + finalSpawn);
    }
    
    void SpawnPointBall()
    {
        float offSet = Random.Range(0f, 14f);
        Vector3 finalSpawn = new Vector3(topSpawn.position.x+offSet, topSpawn.position.y, topSpawn.position.z);
        Instantiate(pointBallPrefab, finalSpawn, topSpawn.rotation);
    }   
}
