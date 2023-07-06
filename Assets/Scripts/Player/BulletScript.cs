using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float time = 3f;
    private bool collided = false;
    public int ammoDamage = 20;
    public AudioClip bulletHitAudio;
    public GameObject bulletTrailPrefab;
    public GameObject bloodEffectPrefab; // Kan efekti prefabý

    private void Start()
    {
        Destroy(gameObject, time);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collided)
        {
            collided = true;
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyHealth>().takeDamage(ammoDamage);
            AudioSource.PlayClipAtPoint(bulletHitAudio, transform.position);

            // Kan efekti oluþtur
            GameObject bloodEffect = Instantiate(bloodEffectPrefab, collision.contacts[0].point, Quaternion.identity);
            Destroy(bloodEffect, 1f); // Kan efektini bir süre sonra yok etmek için
        }
        else
        {
            GameObject bulletTrail = Instantiate(bulletTrailPrefab, transform.position, Quaternion.identity);
            Destroy(bulletTrail, 1f);
        }
    }
}
