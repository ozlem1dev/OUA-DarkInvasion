using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMana : MonoBehaviour
{
    public float maxMana;
    public float currentMana;
    public Image ManaBar;
    //public float manaRegenRate = 2f;

    void Start()
    {
        ResetMana();
    }


    void Update()
    {
        UpdateManaBar();
    }


    public void UpdateManaBar()
    {
        float fill = (float)currentMana / maxMana;
        ManaBar.fillAmount = fill;
    }

    public void ResetMana()
    {
        currentMana = maxMana;
        UpdateManaBar();
    }
}