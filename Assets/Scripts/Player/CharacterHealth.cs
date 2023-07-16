using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHealth : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public Image HealthBar;
    public GameObject menu;
    public GameObject characterPanel;
    Animator _animator;
    public GameObject eventSystem;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        ResetHealth();

    }

    private void Update()
    {
        UpdateHealthBar();
        _animator.SetInteger("Health", currentHealth);
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
    }

    public void UpdateHealthBar()
    {
        float fill = (float)currentHealth / maxHealth;
        HealthBar.fillAmount = fill;
    }
    public void Die()
    {
        eventSystem.GetComponent<Spawner>().isLose = true;
        characterPanel.SetActive(false);
        menu.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void ResetHealth()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }
}