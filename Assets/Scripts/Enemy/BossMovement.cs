using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;
using static UnityEditor.Experimental.GraphView.GraphView;

public class BossMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform[] waypoints;
    public LayerMask ground;
    public float attackCooldown;
    public float  attackRange;
    public float moveSpeed = 3f;
    public GameObject baseObject;
    public int minLvl, maxLvl;
    public float range = 10f;
    public GameObject[] towers;
    private Transform target;
    private int currentWaypointIndex = 0;
    private bool isAttacked;
    private float distanceToTower;
    float temptower;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

    }

    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        baseObject = GameObject.FindWithTag("Base");
    }
    private void Update()
    {
        
        if (target == null)
        {
            Forward();
        }
        if (target != null)
        {

            if (Vector3.Distance(transform.position, target.transform.position) >= attackRange)
            {
                GoToTower();
            }
            else
                Attack();
        }

        /*if (distanceToTower <= attackRange)
        {

            Attack();
        }*/



    }

    void Forward()
    {
        if (currentWaypointIndex < waypoints.Length)
        {
            Vector3 direction = waypoints[currentWaypointIndex].position - transform.position;
            direction.Normalize();

            agent.speed = moveSpeed;

            agent.SetDestination(waypoints[currentWaypointIndex].position);

            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            targetRotation.eulerAngles = new Vector3(0f, targetRotation.eulerAngles.y, 0f);
            transform.rotation = targetRotation;
            if (Vector3.Distance(transform.position, waypoints[waypoints.Length - 1].position) <= 2f)
            {
                //gameObject.GetComponent<EnemyHealth>().currentHealth =0;
                Destroy(gameObject);
                var baseHealth = baseObject.GetComponent<BaseHealth>();
               
            }
            if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) <= 5f)
            {
                currentWaypointIndex++;
                currentWaypointIndex = Mathf.Clamp(currentWaypointIndex, 0, waypoints.Length - 1);
            }
        }
    }

  

    void Attack()
    {

        agent.SetDestination(transform.position);

        if (!isAttacked) {
            StartCoroutine(ChargeAndAttackTower());

            isAttacked = true;
            Invoke("AttackCooldown", attackCooldown);
        }
    }

    void AttackCooldown()
    {
        isAttacked = false;
    }

    private void UpdateTarget()
    {

         towers = GameObject.FindGameObjectsWithTag("Tower");
        GameObject nearestTower = null;
        float minDistance = float.MaxValue; // Minimum mesafeyi baþlangýçta maksimuma ayarla

        foreach (GameObject tower in towers)
        {
            if(tower.GetComponent<TowerAttack>().isAttackStop==false) 
            {
                distanceToTower = Vector3.Distance(transform.position, tower.transform.position);
                
                if (distanceToTower < minDistance)
                {
                    //Debug.Log(distanceToTower);
                    minDistance = distanceToTower;
                    nearestTower = tower;
                }
            }
           

        }

        if (nearestTower != null && Vector3.Distance(transform.position, nearestTower.transform.position) <= range)
        {
            target = nearestTower.transform;
        }
        else
        {
            target = null;
        }
        Debug.Log(target.name);
    }

    private void GoToTower()
    {
        agent.speed = moveSpeed;
        // Hedefi ayarla
        agent.SetDestination(target.position);
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);

    }
    
    public IEnumerator ChargeAndAttackTower()
    {

        yield return new WaitForSeconds(2f);

        target.GetComponent<TowerAttack>().StopAttack();

      
    }

}