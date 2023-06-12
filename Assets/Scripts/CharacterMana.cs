using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMana : MonoBehaviour
{
    public int maxMana = 100;
    public int currentMana;
    public Image ManaBar;

    void Start()
    {
        currentMana = maxMana;
        UpdateManaBar();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Skill(10);
        }
    }
    private void Skill(int amaount)
    {
        currentMana -= 10;
        if (currentMana <= 0)
        {
            currentMana = 0;
        }

        UpdateManaBar();
        Debug.Log("Güncel mana : " + currentMana);
    }
    private void UpdateManaBar()
    {
        float fill = (float)currentMana / maxMana;
        ManaBar.fillAmount = fill;
    }

}
