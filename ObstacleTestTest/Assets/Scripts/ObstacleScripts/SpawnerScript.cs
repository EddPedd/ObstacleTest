using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    //References to other objects and components
    private Transform [] spawnPoints;
    public GameObject obstaclePrefab;
    public ObstacleManager oManager;

    //Variables
    public bool gameHasStarted = false;
    private float timesSinceLastSpawn;
    [SerializeField]
    private float timeBeteweenSpawns;
    private int spawnNumber;

    // Start is called before the first frame update
    void Start()
    {
        int childCount = transform.childCount;
        spawnPoints = new Transform[childCount];

        for (int i=0; i< childCount; i++)
        {
            spawnPoints[i] = transform.GetChild(i);         //0 is top, 1 is left and 2 is right spawnPoint
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(gameHasStarted)
        {
            timesSinceLastSpawn += Time.deltaTime;  
        }
        if(timesSinceLastSpawn >= timeBeteweenSpawns)
        {
            timesSinceLastSpawn = 0;
            RandomObstacle();
        }
    }

    private void RandomObstacle()
    {                                                //OBS!!! next is to create arratys for all variants to make this smoother
        int randomColor = Random.Range(0,3);            // OBS!!!!! ONLY GREEN COLOR DURING TESTING
        int randomSize = Random.Range(0,3);            //Roll random int between 3 possible for random value
        int randomShape = Random.Range(0,2);
        int randomSpawnPoint = Random.Range(0,3);

        ObstacleColor spawnColor = oManager.obstacleColors[randomColor];  //set spawn variables after the random numbers
        ObstacleSize spawnSize = oManager.obstacleSizes[randomSize];
        ObstacleShape spawnShape = oManager.obstacleShapes[randomShape];
        Transform spawnPoint = spawnPoints[randomSpawnPoint];

        float randomOffSet = Random.Range(0, 101);

        SpawnObstacle(spawnColor, spawnSize, spawnShape, spawnPoint, randomOffSet);
        //Debug.Log("randomSize = " + randomSize);      Outdated error
           
    }

    private void SpawnObstacle(ObstacleColor spawnColor, ObstacleSize spawnSize, ObstacleShape spawnShape, Transform spawnPoint, float offSet)
    {
        ObstacleScript obstacle;

        GameObject obstacleObject = Instantiate(obstaclePrefab, spawnPoint.position , spawnPoint.rotation);
        obstacle = obstacleObject.GetComponent<ObstacleScript>();

        if(obstacle != null)
        {
            obstacle.obstacleColor = spawnColor;
            obstacle.obstacleSize = spawnSize;
            obstacle.obstacleShape = spawnShape;
        }  
        else
        {
           Debug.LogError("obstacleObject is without obstacleScript!");
        }
    }

    public void StartGame()
    {
        gameHasStarted = true;
    }

    public void GameOver()
    {
        gameHasStarted = false;
        timesSinceLastSpawn = 0;
    }
}
