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
    public GameObject menu;
    public GameObject characterPanel;
    Animator _animator;


    private void Awake()
    {
        _animator = GetComponent<Animator>();

        currentHealth = maxHealth;
        Debug.Log("Güncel can : " + currentHealth);
    }


    //void Start()
    //{
    //    UpdateHealthBar();
    //}


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
        Debug.Log("Güncel can : " + currentHealth);

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
        
    }
}
