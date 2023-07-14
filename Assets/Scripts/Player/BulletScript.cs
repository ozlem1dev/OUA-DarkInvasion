using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float time;
    private bool collided = false;
    public int ammoDamage;
    public static int currentAmmoDamage = 20;
    public AudioClip bulletHitAudio;
    public GameObject bulletTrailPrefab;
    public GameObject bloodEffectPrefab; // Kan efekti prefabi

    private void Start()
    {
        Debug.Log("AmmoDamage: " + ammoDamage + "CurrentAmmoDamage: " + currentAmmoDamage);
        Destroy(gameObject, time);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyHealth>().takeDamage(currentAmmoDamage);
            AudioSource.PlayClipAtPoint(bulletHitAudio, transform.position);

            // Kan efekti olustur
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