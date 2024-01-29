using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointManagerScript : MonoBehaviour
{
    // Static reference to the instance of the class
    private static PointManagerScript _instance;

    // Public property to access the instance from other scripts
    public static PointManagerScript Instance
    {
        get
        {
            // If the instance doesn't exist, find or create it
            if (_instance == null)
            {
                _instance = FindObjectOfType<PointManagerScript>();
            }

            return _instance;
        }
    }

    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI multiplierText;

    private bool gameHasStarted;
    private float currentPoints;
    private int currentMultiplier;

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
    
    public void UpdatePoints(int points)
    {
        if(gameHasStarted)
        {
            currentPoints += points*currentMultiplier;
            string pointString = currentPoints.ToString();
            scoreText.text = pointString;
            Debug.Log("Current points were updated. Current points is = " + currentPoints);
        }
    }

    public void UpdateMultiplier(int multiplier)
    {
        if(gameHasStarted)
        {
            currentMultiplier += multiplier;
            string multiplierString = currentMultiplier.ToString();
            multiplierText.text = (multiplierString + "x");
        }
    }
    
    public void StartGame()
    {
        gameHasStarted = true;
        currentPoints = 0;
        currentMultiplier = 0;
        UpdateMultiplier(1);
        UpdatePoints(0);
    }

    public void GameOver()
    {
        gameHasStarted = false;
    }
}
