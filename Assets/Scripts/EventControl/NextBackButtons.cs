using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class NextBackButtons : MonoBehaviour
{
    public GameObject eventTower;
    TowerPoints towerPoints;
    Button button;
    void Start()
    {
        towerPoints = eventTower.GetComponent<TowerPoints>();
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            WhichButton();
        });
    }

    void WhichButton()
    {
        
        if (button.name == "Next")
        {
            towerPoints.Next();
            
        }
        if (button.name == "Back")
        {
            towerPoints.Back();
        }
    }
}