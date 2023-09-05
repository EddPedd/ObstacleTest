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
        int randomColor = Random.Range(0,3);

        if(randomColor == 0){
            SpawnObstacle(oManager.GreenColorInstance);
        }
        else if(randomColor ==1)
        {
            SpawnObstacle(oManager.BlueColorInstance);
        }
        else if (randomColor ==2)
        {
            SpawnObstacle(oManager.RedColorInstance);
        }

        Debug.Log("randomColor = " + randomColor);
    }

    private void SpawnObstacle(ObstacleColor spawnColor)
    {
        ObstacleScript obstacle;
        GameObject obstacleObject = Instantiate(obstaclePrefab, spawnPosition.position , spawnPosition.rotation);

        obstacle = obstacleObject.GetComponent<ObstacleScript>();

        if(obstacle != null)
        {
            obstacle.obstacleColor = spawnColor;
        }  
        else{
            Debug.LogError("obstacleObject is without obstacleScript!");
        }
        
        //obstacle.obstacleColor = 
    }
}
