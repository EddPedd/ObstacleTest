using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    
    public Image health1;
    public Image health2;
    
    private int currentHealth = 0; 
    
    // Static reference to the instance of the class
    private static HealthManager _instance;

    // Public property to access the instance from other scripts
    public static HealthManager Instance
    {
        get
        {
            // If the instance doesn't exist, find or create it
            if (_instance == null)
            {
                _instance = FindObjectOfType<HealthManager>();
            }

            return _instance;
        }
    }

    // Your class implementation here

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
        ChangeHealth(1);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("t"))
        {
            ChangeHealth(1);
        }
        if(Input.GetKeyDown("r"))
        {
            ChangeHealth(-1);
        }
    }

    public void ChangeHealth(int health)
    {
        //Calculate new health
        currentHealth += health;
        currentHealth = Mathf.Clamp(currentHealth, 0, 2);

        //Change health UI
        if(currentHealth == 2)
        {
            health1.enabled=true;
            health2.enabled=true;
        }
        else if(currentHealth == 1)     
        {
            health1.enabled=true;
            health2.enabled=false;
        }
        else if (currentHealth == 0)     //Avsluta spelet
        {
            GameManager.Instance.GameOver();
            health1.enabled=false;
            health2.enabled=false;
        }

        Debug.Log("Player Health is equal to " + currentHealth);
    }

    public void SetHealth(int health)
    {
        //Calculate new health
        currentHealth = health;
        currentHealth = Mathf.Clamp(currentHealth, 0, 2);

        //Change health UI
        if(currentHealth == 2)
        {
            health1.enabled=true;
            health2.enabled=true;
        }
        else if(currentHealth == 1)     
        {
            health1.enabled=true;
            health2.enabled=false;
        }
        else if (currentHealth == 0)     //Avsluta spelet
        {
            GameManager.Instance.GameOver();
            health1.enabled=false;
            health2.enabled=false;
        }

        Debug.Log("Player Health is equal to " + currentHealth); 
    }
}
