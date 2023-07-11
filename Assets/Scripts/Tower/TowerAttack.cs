using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class TowerAttack : MonoBehaviour
{
    public float range = 15f;
    public float fireRate = 1f;

    public GameObject bulletPre;
    public Transform firePoint;

    public bool isAttackStop = false;
    private float stopAttack = 7f;
    public Transform partRotate;
    public float turnSpeed=5f;

    public bool canTurn;

    private Transform target;
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

        if(canTurn) 
        {
            Vector3 dir = transform.position - target.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(partRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
            partRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }
        
        if (fireCountdown <= 0f && isAttackStop == false)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        Vector3 _rotation = new Vector3(firePoint.rotation.x, firePoint.rotation.y, firePoint.rotation.z - 120);
        GameObject bulletObject = Instantiate(bulletPre, firePoint.position, firePoint.rotation);
        bulletObject.transform.rotation = Quaternion.Euler(_rotation);
        TowerBullet bullet = bulletObject.GetComponent<TowerBullet>();

        if (bullet != null)
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
            target = nearestEnemy.transform;
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

    public void StopAttack()
    {
       isAttackStop = true;
        StartCoroutine(PreventAttack());


    }
    public IEnumerator PreventAttack()
    {

        yield return new WaitForSeconds(stopAttack);

        isAttackStop = false;


    }
}
