using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    public float x = 450;
    public float y = 3;

    private float sensitivity = 0.5f;

    private void Start()
    {
        var cinemachine = GetComponent<Cinemachine.CinemachineFreeLook>();
        cinemachine.m_XAxis.m_MaxSpeed = x * sensitivity;
        cinemachine.m_YAxis.m_MaxSpeed = y * sensitivity;
    }

    private void Update()
    {

    }

    public void SetSensitivity(float value)
    {
        sensitivity = value;
        var cinemachine = GetComponent<Cinemachine.CinemachineFreeLook>();
        cinemachine.m_XAxis.m_MaxSpeed = x * sensitivity;
        cinemachine.m_YAxis.m_MaxSpeed = y * sensitivity;
        Debug.Log("Slider de�eri: " + cinemachine.m_XAxis.m_MaxSpeed);
    }
}
