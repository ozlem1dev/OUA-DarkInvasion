using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Image HealthBar;
    public Camera cam;
    public Transform canvas;

    void Start()
    {
        cam=Camera.main;
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    private void Update()
    {
        canvas.transform.LookAt(canvas.transform.position + cam.transform.forward);
    }
    public void takeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
            Debug.Log("Düþman öldü");
        }

        UpdateHealthBar();
        Debug.Log("Düþman güncel can : " + currentHealth);
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

