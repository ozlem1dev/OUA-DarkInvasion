using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMana : MonoBehaviour
{
    public float maxMana = 100f;
    public float currentMana;
    public Image ManaBar;
    public float manaRegenRate = 2f;

    void Start()
    {
        currentMana = maxMana;
        UpdateManaBar();
    }


    void Update()
    {
        if(currentMana < maxMana)
        {
            currentMana += manaRegenRate * Time.deltaTime;
        }

        if(currentMana > maxMana)
        {
            currentMana = maxMana;
        }
        
        if (Input.GetKeyDown(KeyCode.M))
        {
            Skill(10);
        }
        UpdateManaBar();
    }
    public void Skill(float amaount)
    {
        currentMana -= amaount;
        if (currentMana <= 0)
        {
            currentMana = 0;
        }

        UpdateManaBar();
        //Debug.Log("Güncel mana : " + currentMana);
    }
    public void UpdateManaBar()
    {
        float fill = (float)currentMana / maxMana;
        ManaBar.fillAmount = fill;
    }

    public float getCurrentMana()
    {
        return currentMana;
    }
}
