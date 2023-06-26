using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;

public class EnemyMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform[] waypoints;
    [SerializeField] public Transform player;
    public LayerMask ground, player0;
    public float attackCooldown;
    public float sightRange, attackRange;
    public bool inSight, inRange;
    public float moveSpeed = 3f;
    public int attackDamage = 10;
    public GameObject baseObject;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public bool isRanged;
    public int minLvl, maxLvl;
    private int currentWaypointIndex = 0;
    private bool isAttacked;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

    }

    private void Start()
    {
        baseObject = GameObject.FindWithTag("Base");
    }

    private void Update()
    {

        inSight = Physics.CheckSphere(transform.position, sightRange, player0);
        inRange = Physics.CheckSphere(transform.position, attackRange, player0);

        if (!inSight && !inRange)
        {

            Forward();
        }
        else if (inSight && !inRange)
        {
            ChasePlayer();
        }
        else if (inSight && inRange)
        {
            Attack();
        }

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
                baseHealth.takeDamage(attackDamage);
            }
            if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) <= 5f)
            {
                currentWaypointIndex++;
                currentWaypointIndex = Mathf.Clamp(currentWaypointIndex, 0, waypoints.Length - 1);
            }
        }
    }

    void ChasePlayer()
    {
        agent.speed = moveSpeed;
        // Hedefi ayarla
        agent.SetDestination(player.position);
    }

    void Attack()
    {
        transform.LookAt(player);
        var playerHealth = player.GetComponent<CharacterHealth>();
        agent.SetDestination(transform.position);


        if (!isAttacked && playerHealth != null)
        {
            if (isRanged)
            {
                GameObject sphere = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);


                Vector3 direction = player.transform.position - transform.position;
                direction.Normalize();



                Rigidbody sphereRigidbody = sphere.GetComponent<Rigidbody>();
                sphereRigidbody.velocity = direction * 10f;
                Debug.Log(sphereRigidbody.velocity);
            }
            else if (!isRanged)
            {
                
                playerHealth.takeDamage(attackDamage);
            }

            isAttacked = true;
            Invoke("AttackCooldown", attackCooldown);
        }
    }

    void AttackCooldown()
    {
        isAttacked = false;
    }
}