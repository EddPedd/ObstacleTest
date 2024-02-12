using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Static reference to the instance of the class
    private static GameManager _instance;

    // Public property to access the instance from other scripts
    public static GameManager Instance
    {
        get
        {
            // If the instance doesn't exist, find or create it
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
            }

            return _instance;
        }
    }
    
    private List<GameObject> scoreObjects; 

    [SerializeField]
    private GameObject startMenu;

    [SerializeField]
    private SpawnerScript spawner;

    void Start ()
    {
        GameObject spawnerObject = GameObject.FindGameObjectWithTag("Spawner");

        spawner = spawnerObject.GetComponent<SpawnerScript>();
        
        Debug.Log("spawner = " + spawner + "and pointSpawner = ");

    }
    
    public void StartGame()
    {
        startMenu.SetActive(false);
        
        spawner.StartGame();

        PointManagerScript.Instance.StartGame();

        HealthManager.Instance.SetHealth(1);
    }

    public void GameOver()
    {
        startMenu.SetActive(true);
        spawner.GameOver();
        PointManagerScript.Instance.GameOver();
    }
}
