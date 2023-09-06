using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    //References to other objects and components
    private Transform spawnPosition;
    public GameObject obstaclePrefab;
    public ObstacleManager oManager;

    //Variables
    private float timesSinceLastSpawn;
    [SerializeField]
    private float timeBeteweenSpawns;
    private int spawnNumber;

    // Start is called before the first frame update
    void Start()
    {
        Transform foundChild = transform.Find("Spawnpoint");
        if(foundChild != null)
        {
            spawnPosition = foundChild;
        }
        else
        {
            Debug.LogError("Child object not found!");
        }

        RandomObstacle();
    }

    // Update is called once per frame
    void Update()
    {
        timesSinceLastSpawn += Time.deltaTime;
        if(timesSinceLastSpawn >= timeBeteweenSpawns)
        {
            timesSinceLastSpawn = 0;
            RandomObstacle();
        }
    }

    private void RandomObstacle(){
        ObstacleColor spawnColor = default ;      //Create
        ObstacleSize spawnSize = default; 

        int randomColor = Random.Range(0,3);            //Roll random int between 3 possible
        int randomSize = Random.Range(0,3);

        
        if(randomColor == 0){                           //Set Color
            spawnColor = oManager.GreenColorInstance;
        }
        else if(randomColor ==1)
        {
            spawnColor = oManager.BlueColorInstance;
        }
        else if (randomColor ==2)
        {
            spawnColor = oManager.RedColorInstance;
        }

        if(randomSize == 0){
            spawnSize = oManager.SmallSizeInstance;
        }
        else if(randomSize ==1)
        {
            spawnSize = oManager.MediumSizeInstance;
        }
        else if (randomSize ==2)
        {
            spawnSize = oManager.LargeSizeInstance;
        }

        SpawnObstacle(spawnColor, spawnSize);
        //Debug.Log("randomSize = " + randomSize);      Outdated error
        //Debug.Log     
    }

    private void SpawnObstacle(ObstacleColor spawnColor, ObstacleSize spawnSize)
    {
        ObstacleScript obstacle;
        GameObject obstacleObject = Instantiate(obstaclePrefab, spawnPosition.position , spawnPosition.rotation);

        obstacle = obstacleObject.GetComponent<ObstacleScript>();

        if(obstacle != null)
        {
            obstacle.obstacleColor = spawnColor;
            obstacle.obstacleSize = spawnSize;
        }  
        else{
            Debug.LogError("obstacleObject is without obstacleScript!");
        }
        
        //obstacle.obstacleColor = 
    }
}
