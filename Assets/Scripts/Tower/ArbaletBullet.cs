using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArbaletBullet : MonoBehaviour
{
    public float speed;
    public float damage;
    public static float currentDamage = 25;
    private Transform target;
    public GameObject arbaletEffect;

    private void Start()
    {
        Debug.Log("ArbaletDamage: " + damage + "CurrentDamage: " + currentDamage);
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
            Quaternion rotation = Quaternion.LookRotation(gameObject.transform.forward);
            GameObject arbalet = Instantiate(arbaletEffect, collision.contacts[0].point, Quaternion.Inverse(rotation), collision.gameObject.transform);
            Destroy(arbalet, 1f); // Kan efektini bir süre sonra yok etmek için
        }
    }
}
