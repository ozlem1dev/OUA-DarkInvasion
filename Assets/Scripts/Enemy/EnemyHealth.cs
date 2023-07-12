using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public float currentHealth;
    public Image HealthBar;


    void Start()
    {

        currentHealth = maxHealth;
        UpdateHealthBar();
    }


    public void takeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
            Debug.Log("Düsman öldü");
        }

        UpdateHealthBar();
        Debug.Log("Düsman güncel can : " + currentHealth);
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