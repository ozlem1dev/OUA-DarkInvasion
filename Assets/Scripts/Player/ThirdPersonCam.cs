using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{   
    public static float x { get; } = 450;
    public static float y { get; } = 3;
    public float slider = 0.5f;

    private void Start()
    {
        //cursor invisibility
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


        
        var cinemachine = gameObject.GetComponent<Cinemachine.CinemachineFreeLook>();
        cinemachine.m_XAxis.m_MaxSpeed = x * slider;
        cinemachine.m_YAxis.m_MaxSpeed = y * slider;
    }

    private void Update()
    {
         
    }

}
