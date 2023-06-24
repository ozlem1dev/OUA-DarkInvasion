using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairController : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public RectTransform crosshair;

    private void Update()
    {
        // BulletSpawnPoint'ýn dünya koordinatlarýný ekran koordinatlarýna dönüþtür.
        Vector3 screenPos = Camera.main.WorldToScreenPoint(bulletSpawnPoint.position);

        // Crosshair'in konumunu güncelle.
        crosshair.position = screenPos;
    }
}
