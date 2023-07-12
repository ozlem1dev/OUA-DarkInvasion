using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class BaseHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Image HealthBar;
    public GameObject menu;
    public GameObject characterPanel;
    public GameObject eventSystem;

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

    public void UpdateHealthBar()
    {
        float fill = (float)currentHealth / maxHealth;
        HealthBar.fillAmount = fill;
    }

    public void Die()
    {
        characterPanel.SetActive(false);
        menu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        eventSystem.GetComponent<Spawner>().isLose = true;
    }

    public void ResetHealth()
    {
        UpdateHealthBar();
        currentHealth = maxHealth;
    }


}