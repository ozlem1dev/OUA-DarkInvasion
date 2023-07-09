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
            Quaternion rotation = Quaternion.LookRotation(gameObject.transform.forward);
            GameObject bloodEffect = Instantiate(bloodEffectPrefab, collision.contacts[0].point, Quaternion.Inverse(rotation), collision.gameObject.transform);
            Destroy(bloodEffect, 5f); // Kan efektini bir süre sonra yok etmek için
        }
        
         //collision.gameObject.transform.position
        else
        {
            GameObject bulletTrail = Instantiate(bulletTrailPrefab, transform.position, Quaternion.identity);
            Destroy(bulletTrail, 1f);
        }
    }
}
