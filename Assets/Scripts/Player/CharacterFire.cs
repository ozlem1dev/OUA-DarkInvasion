using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

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
    public float fireCooldown = 0.2f;
    private float nextFireTime = 0f;
    public AudioClip fireAudio;
    public AudioClip reloadAudio;

    public GameObject muzzleFlashPrefab;
    public Transform muzzleFlashSpawnPoint;




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

        if (Input.GetMouseButton(0) && currentClip > 0 && Time.time >= nextFireTime)
        {
            Fire();
            UpdateAmmoText();
            nextFireTime = Time.time + fireCooldown; // Bir sonraki ateþ etme zamanýný ayarla
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
        Vector3 fireDirection = bulletSpawnPoint.forward + bulletSpawnPoint.up * Mathf.Tan(Mathf.Deg2Rad * (15f - Camera.main.transform.eulerAngles.x));
        bullet.GetComponent<Rigidbody>().velocity = fireDirection.normalized * bulletSpeed;

        currentClip--;

        // Muzzle flash efektini oluþtur
        var muzzleFlash = Instantiate(muzzleFlashPrefab, muzzleFlashSpawnPoint.position, muzzleFlashSpawnPoint.rotation);

        // Muzzle flash efektini bir süre sonra yok et
        Destroy(muzzleFlash, 0.1f);

        AudioSource.PlayClipAtPoint(fireAudio, transform.position);
    }


    private IEnumerator Reload()
    {
        isReloading = true;
        // Þarjör deðiþme animasyonu oynatýlabilir.
        AudioSource.PlayClipAtPoint(reloadAudio, transform.position);

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