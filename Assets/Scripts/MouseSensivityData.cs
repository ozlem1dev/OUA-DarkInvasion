using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SensitivityData", menuName = "Data/Sensitivity Data")]
public class MouseSensivityData : ScriptableObject
{
    public float sensitivityValue = 0.5f;
    public float soundSensitivityValue = 0.5f;
}
