using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterFire : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;

    public int maxAmmo = 210;
    public int clipSize = 30;
    private int currentAmmo;
    private int currentClip;
    private bool isReloading = false;
    public Text ammoText;

    private void Start()
    {
        currentAmmo = maxAmmo;
        currentClip = clipSize;
        UpdateAmmoText();
    }
    void Update()
    {
        if (isReloading)
            return;

        if (Input.GetMouseButtonDown(0) && currentClip > 0)
        {
            Fire();
            UpdateAmmoText();
        }

        if ((Input.GetKeyDown(KeyCode.R) || (currentClip == 0)) && currentAmmo > 0 && currentClip < clipSize)
        {
            StartCoroutine(Reload());
            UpdateAmmoText();
        }
    }


    private void Fire()
    {
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
        currentClip--;
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        // Þarjör deðiþme animasyonu oynatýlabilir.

        yield return new WaitForSeconds(1f); // Örnek olarak 1 saniye bekleme süresi.

        int ammoNeeded = clipSize - currentClip;
        int ammoToReload = Mathf.Min(ammoNeeded, currentAmmo);
        currentAmmo -= ammoToReload;
        currentClip += ammoToReload;

        isReloading = false;
        UpdateAmmoText();
    }

    private void UpdateAmmoText()
    {
        ammoText.text = string.Format("{0}/{1}", currentClip, currentAmmo);
    }

}