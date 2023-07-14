using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    public float x = 450f;
    public float y = 3f;
    private float sensitivity = 0.5f;
    Cinemachine.CinemachineFreeLook cinemachine;

    private void Start()
    {
        cinemachine = gameObject.GetComponent<Cinemachine.CinemachineFreeLook>();
        cinemachine.m_XAxis.m_MaxSpeed = x * sensitivity;
        cinemachine.m_YAxis.m_MaxSpeed = y * sensitivity;
    }

    private void Update()
    {

    }

    public void SetSensitivity(float value)
    {
        sensitivity = value;

        cinemachine.m_XAxis.m_MaxSpeed = x * sensitivity;
        cinemachine.m_YAxis.m_MaxSpeed = y * sensitivity;
        Debug.Log("Slider degeri: " + cinemachine.m_XAxis.m_MaxSpeed);
    }
}

