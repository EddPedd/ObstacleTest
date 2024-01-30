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

    private void RandomObstacle(){                  //OBS!!! next is to create arratys for all variants to make this smoother
        ObstacleColor spawnColor = default ;      //Create variables
        ObstacleSize spawnSize = default; 
        Transform spawnPoint = default;

        int randomColor = Random.Range(0,1);            // OBS!!!!! ONLY GREEN COLOR DURING TESTING
        int randomSize = Random.Range(0,3);            //Roll random int between 3 possible for random value
        int randomSpawnPoint = Random.Range(0,3);

        spawnColor = oManager.obstacleColors[randomColor];  //set spawn variables after the random numbers
        spawnSize = oManager.obstacleSizes[randomSize];
        spawnPoint = spawnPoints[randomSpawnPoint];

        float randomOffSet = Random.Range(0, 101);

        SpawnObstacle(spawnColor, spawnSize, spawnPoint, randomOffSet);
        //Debug.Log("randomSize = " + randomSize);      Outdated error
           
    }

    private void SpawnObstacle(ObstacleColor spawnColor, ObstacleSize spawnSize, Transform spawnPoint, float offSet)
    {
        ObstacleScript obstacle;

        float finalOffSet = default;
        Vector3 finalOffSetPosition = default;
        
        if(spawnPoint.position.x<-10f || spawnPoint.position.x> 10f) //if spawn is one of the side ones
        {
            finalOffSet = (offSet*8f)/100f;
            finalOffSetPosition = new Vector3 (spawnPoint.position.x, spawnPoint.position.y + finalOffSet, spawnPoint.position.z);
        }
        else
        {
            finalOffSet = (offSet*14f)/100f;
            finalOffSetPosition = new Vector3 (spawnPoint.position.x + finalOffSet, spawnPoint.position.y, spawnPoint.position.z);
        }
      
        GameObject obstacleObject = Instantiate(obstaclePrefab, finalOffSetPosition , spawnPoint.rotation);
        obstacle = obstacleObject.GetComponent<ObstacleScript>();

        if(obstacle != null)
        {
            obstacle.obstacleColor = spawnColor;
            obstacle.obstacleSize = spawnSize;
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
