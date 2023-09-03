using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    //References to other objects and components
    private Transform spawnPosition;
    public GameObject obstaclePrefab;

    //Variables
    private float timesSinceLastSpawn;
    [SerializeField]
    private float timeBeteweenSpawns;

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

        SpawnObstacle();
    }

    // Update is called once per frame
    void Update()
    {
        timesSinceLastSpawn += Time.deltaTime;
        if(timesSinceLastSpawn >= timeBeteweenSpawns)
        {
            timesSinceLastSpawn = 0;
            SpawnObstacle();
        }
    }

    private void SpawnObstacle()
    {
        Instantiate(obstaclePrefab, spawnPosition.position, spawnPosition.rotation);        //This method creates a new Obstacle dad.
    }
}
