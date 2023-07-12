using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairController : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public Transform crosshair;

    void Update()
    {
        // Mermi ateþleme yönünü hesapla
        Vector3 fireDirection = bulletSpawnPoint.forward + bulletSpawnPoint.up * Mathf.Tan(Mathf.Deg2Rad * (15f - Camera.main.transform.eulerAngles.x));

        // Mermi spawn noktasýnýn pozisyonunu güncelle
        bulletSpawnPoint.position = crosshair.position;

        // Mermi ateþleme yönünü güncelle
        bulletSpawnPoint.rotation = Quaternion.LookRotation(fireDirection, Vector3.up);
    }
}