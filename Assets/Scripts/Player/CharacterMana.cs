using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMana : MonoBehaviour
{
    public float maxMana = 100f;
    public float currentMana;
    public Image ManaBar;
    //public float manaRegenRate = 2f;

    void Start()
    {
        currentMana = maxMana;
        UpdateManaBar();
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


}
