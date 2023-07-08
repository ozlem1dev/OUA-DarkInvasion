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
