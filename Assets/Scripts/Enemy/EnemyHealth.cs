using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth;
    public Image HealthBar;
    public GameObject canvas;
    
    void Start()
    {
       
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    private void Update()
    {
        canvas.transform.rotation = Quaternion.LookRotation(canvas.transform.position - Camera.main.transform.position);
        
    }
    public void takeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
            
        }

        UpdateHealthBar();
        
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