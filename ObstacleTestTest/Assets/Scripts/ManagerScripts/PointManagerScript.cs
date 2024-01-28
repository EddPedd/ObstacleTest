using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private float currentPoints;

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
        currentPoints += points;
        Debug.Log("Current points were updated. Current points is = " + currentPoints);
    }
}
