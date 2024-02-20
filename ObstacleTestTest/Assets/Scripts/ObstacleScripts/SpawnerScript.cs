using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    // Static reference to the instance of the class
    private static SpawnerScript _instance;

    // Public property to access the instance from other scripts
    public static SpawnerScript Instance
    {
        get
        {
            // If the instance doesn't exist, find or create it
            if (_instance == null)
            {
                _instance = FindObjectOfType<SpawnerScript>();
            }

            return _instance;
        }
    }
    
    //References to other objects and components
    private Transform [] spawnPoints;
    public GameObject obstaclePrefab;
    public ObstacleManager oManager;
    private Transform pTransform;

    //Variables
    public bool gameHasStarted = false;
    private float timesSinceLastSpawn;
    [SerializeField]
    private float timeBeteweenSpawns;
    private int spawnNumber;
    [SerializeField]
    public bool activeCube = false;
    
    

    private void Awake()
    {
        // Ensure there's only one instance
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }

        // Set the instance to this object
        _instance = this;
        
        // Don't destroy the GameObject when loading a new scene
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        pTransform = player.GetComponent<Transform>();
        
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
            //OverHeadTriangle();
        }
    }

    private void OverHeadTriangle()
    {
        int randomColor = Random.Range(0,3);            
        int randomSize = Random.Range(0,3);            //Roll random int between 3 possible for random value 
        float playerOffSet = (100f*pTransform.position.x)/14f;

        ObstacleColor spawnColor = oManager.obstacleColors[randomColor];  //set spawn variables after the random numbers
        ObstacleSize spawnSize = oManager.obstacleSizes[randomSize];
        ObstacleShape spawnShape = oManager.obstacleShapes[1];              //two for triangle
        Transform spawnPoint = spawnPoints[0];                              //0 for top spawnpoint

        SpawnObstacle(spawnColor, spawnSize, spawnShape, spawnPoint, playerOffSet);
    }
    
    
    private void RandomObstacle()
    { 
        int randomColor = Random.Range(0,3);            
        int randomSize = Random.Range(0,3);            //Roll random int between 3 possible for random value
        
        int randomShape = default;
        if (activeCube)
        {
            randomShape = Random.Range(0,2);
        }
        else
        {
            randomShape = Random.Range(0,3);
        }
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
        GameObject obstacleObject;

        if(spawnPoint.position.x > 0 || spawnPoint.position.x < -8)
        {
            obstacleObject = Instantiate(obstaclePrefab, spawnPoint.position , spawnPoint.rotation);
        }
        else
        {
            Vector3 finalSpawnPosition = new Vector3(spawnPoint.position.x + (offSet*14/100), spawnPoint.position.y, spawnPoint.position.z);
            obstacleObject = Instantiate(obstaclePrefab, finalSpawnPosition , spawnPoint.rotation);
        }

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
