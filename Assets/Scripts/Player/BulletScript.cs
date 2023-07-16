using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float time;
    private bool collided = false;
    public int ammoDamage;
    public static int currentAmmoDamage = 20;
    public GameObject bulletHitAudioPrefab;
    public GameObject bulletTrailPrefab;
    public GameObject bloodEffectPrefab; // Kan efekti prefabi

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyHealth>().takeDamage(currentAmmoDamage);
            // Kan efekti olustur
            Quaternion rotation = Quaternion.LookRotation(gameObject.transform.forward);
            GameObject bloodEffect = Instantiate(bloodEffectPrefab, collision.contacts[0].point, Quaternion.Inverse(rotation), collision.gameObject.transform);
            GameObject bulletHitAudio = Instantiate(bulletHitAudioPrefab, collision.contacts[0].point, Quaternion.identity, collision.gameObject.transform);
            Destroy(bloodEffect, 5f); // Kan efektini bir süre sonra yok etmek için
            Destroy(bulletHitAudio, 3f);
        }

        else
        {
            GameObject bulletTrail = Instantiate(bulletTrailPrefab, transform.position, Quaternion.identity);
            Destroy(bulletTrail, 1f);
        }

        if (!collided)
        {
            collided = true;
            Destroy(gameObject);
        }
    }

    public void UpdateAmmoDamage(int newDamage)
    {
        currentAmmoDamage = (100 + newDamage) * currentAmmoDamage / 100;
    }
}