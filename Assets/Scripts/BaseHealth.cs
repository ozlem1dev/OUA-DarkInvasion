using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Image HealthBar;


    void Start()
    {

        currentHealth = maxHealth;
        UpdateHealthBar();
    }


    public void takeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
            
        }

        UpdateHealthBar();
        Debug.Log("Base güncel can : " + currentHealth);

    }

    private void UpdateHealthBar()
    {
        float fill = (float)currentHealth / maxHealth;
        HealthBar.fillAmount = fill;
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }
}
