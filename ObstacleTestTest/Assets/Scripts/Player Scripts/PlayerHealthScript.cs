using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthScript : MonoBehaviour
{
    private int currentHealth; 
    
    void Start()
    {
        currentHealth = 1;
    }

    void Update()
    {
        if(Input.GetKeyDown("t"))
        {
            ChangeHealth(1);
        }
    }

    public void ChangeHealth(int health)
    {
        currentHealth += health;
        Debug.Log("Player Health is equal to " + currentHealth);
    }
}
