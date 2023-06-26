using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Image HealthBar;
    private void Awake()
    {
        currentHealth = maxHealth;
        Debug.Log("Güncel can : " + currentHealth);
    }
    void Start()
    {
        
        
        UpdateHealthBar();
    }


    
    public void takeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }

        UpdateHealthBar();
        Debug.Log("Güncel can : " + currentHealth);

    }

    private void UpdateHealthBar()
    {
        float fill = (float)currentHealth / maxHealth;
        HealthBar.fillAmount = fill;
    }

}
