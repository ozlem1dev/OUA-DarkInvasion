using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopcuBullet : MonoBehaviour
{
    public float speed;
    public float damage;
    public static float currentDamage = 45;
    private Transform target;

    public GameObject boomEffect;

    private void Start()
    {
        Debug.Log("TopcuDamage: " + damage + "CurrentDamage: " + currentDamage);
    }
    public void Seek(Transform _target)
    {
        target = _target;
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        var enemyHealth = target.GetComponent<EnemyHealth>();
        enemyHealth.takeDamage(currentDamage);
        Destroy(gameObject);
    }
    public void setDamage(float newDamage)
    {
        currentDamage = (100 + newDamage) * currentDamage / 100;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //collision.gameObject.GetComponent<EnemyHealth>().takeDamage(currentAmmoDamage);
            //AudioSource.PlayClipAtPoint(bulletHitAudio, transform.position);

            // Kan efekti olustur
            Quaternion rotation = Quaternion.LookRotation(gameObject.transform.forward);
            GameObject boom = Instantiate(boomEffect, collision.contacts[0].point, Quaternion.Inverse(rotation), collision.gameObject.transform);
            Destroy(boom, 5f); // Kan efektini bir süre sonra yok etmek için
        }
    }
}
