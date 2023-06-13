using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Image HealthBar;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            takeDamage(10);
        }
    }
    private void takeDamage(int amaount)
    {
        currentHealth -= 10;
        if(currentHealth <=0 )
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
