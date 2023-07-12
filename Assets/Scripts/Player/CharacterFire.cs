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

    public int clipSize;
    public int currentClip;
    public bool isReloading = false;
    public Text ammoText;
    public float fireCooldown = 0.2f;
    private float nextFireTime = 0f;
    public AudioClip fireAudio;
    public AudioClip reloadAudio;

    public GameObject muzzleFlashPrefab;
    public Transform muzzleFlashSpawnPoint;

    Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }


    private void Start()
    {
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
            nextFireTime = Time.time + fireCooldown; // Bir sonraki ateş etme zamanını ayarla
        }

        if ((Input.GetKeyDown(KeyCode.R) || (currentClip == 0)) && currentClip < clipSize)
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

        // Muzzle flash efektini oluştur
        var muzzleFlash = Instantiate(muzzleFlashPrefab, muzzleFlashSpawnPoint.position, muzzleFlashSpawnPoint.rotation);

        // Muzzle flash efektini bir süre sonra yok et
        Destroy(muzzleFlash, 0.1f);

        AudioSource.PlayClipAtPoint(fireAudio, transform.position);
    }


    private IEnumerator Reload()
    {
        isReloading = true;
        _animator.SetBool("Reloading", isReloading);
        // Şarjör değişme animasyonu oynatılabilir.
        AudioSource.PlayClipAtPoint(reloadAudio, transform.position);

        yield return new WaitForSeconds(2f); // Örnek olarak 1 saniye bekleme süresi.

        int ammoNeeded = clipSize - currentClip;
        int ammoToReload = ammoNeeded;
        currentClip += ammoToReload;

        isReloading = false;
        _animator.SetBool("Reloading", isReloading);
        UpdateAmmoText();
    }

    public void UpdateAmmoText()
    {
        ammoText.text = string.Format("{0}/{1}", currentClip, "∞");
    }

    public void ResetAmmo()
    {
        isReloading = false;
        currentClip = clipSize;
        UpdateAmmoText();
    }

}