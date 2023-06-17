using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class TowerAttack : MonoBehaviour
{


    
    public float range = 15f;
    public float fireRate=1f;
    public GameObject bulletPre;
    public Transform firePoint;

    /*public Transform partRotate;
    public float turnSpeed=5f;*/

    private Transform target;
    private string enemyTag = "Enemy";
    private float fireCountdown = 0f;


    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void Update()
    {
        if (target == null)
        {
            return;
        }
        
        /*Vector3 dir= transform.position - target.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partRotate.rotation,lookRotation,Time.deltaTime*turnSpeed).eulerAngles;
        partRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);*/

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }
        fireCountdown-=Time.deltaTime;
    }

    void Shoot()
    {
        GameObject bulletGO=Instantiate(bulletPre, firePoint.position,firePoint.rotation);
        TowerBullet bullet = bulletGO.GetComponent<TowerBullet>();

        if(bullet != null)
        {
            bullet.Seek(target);
        }
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }

        }
        if (nearestEnemy != null && shortestDistance <= range)
        {
            target= nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
