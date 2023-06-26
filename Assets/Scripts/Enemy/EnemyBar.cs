using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBar : MonoBehaviour
{
    private Camera cam;
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position-cam.transform.position);
    }
}
