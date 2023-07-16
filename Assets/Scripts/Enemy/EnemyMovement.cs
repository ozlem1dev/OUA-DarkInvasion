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
    public Transform player;
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
    public bool spawned1;
    public bool spawned2;
    public bool spawned3;
    CharacterHealth playerHealth;

    GameObject soldier;


    private int currentWaypointIndex = 0;
    private bool isAttacked;

    Animator _animator;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

    }

    private void Start()
    {
        baseObject = GameObject.FindWithTag("Base");
        soldier = GameObject.Find("Soldier_demo");
        playerHealth = soldier.GetComponent<CharacterHealth>();
    }

    private void Update()
    {
        inSight = Physics.CheckSphere(transform.position, sightRange, player0);
        inRange = Physics.CheckSphere(transform.position, attackRange, player0);

        if (!inSight && !inRange)
        {
            Forward();
            _animator.SetFloat("Speed", agent.speed);
        }
        else if (inSight && !inRange && playerHealth.currentHealth > 0)
        {
            ChasePlayer();
            _animator.SetFloat("Speed", agent.speed);
        }
        else if (inSight && inRange && playerHealth.currentHealth > 0)
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
        agent.SetDestination(transform.position);

        if (!isAttacked && playerHealth != null)
        {
            if (isRanged)
            {
                
                //Vector3 _rotation = new Vector3(firePoint.rotation.x, firePoint.rotation.y, firePoint.rotation.z - 90);
                GameObject arrow = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
                //arrow.transform.rotation = Quaternion.Euler(_rotation);

                Vector3 direction = player.position - transform.position;
                direction.Normalize();


                Rigidbody arrowRigidbody = arrow.GetComponent<Rigidbody>();
                arrowRigidbody.velocity = direction * 20f;
                //Debug.Log(sphereRigidbody.velocity);
                _animator.SetBool("Attack", true);
            }
            else if (!isRanged)
            {
                _animator.SetBool("Attack", true);
                playerHealth.takeDamage(attackDamage);
            }

            isAttacked = true;
            Invoke("AttackCooldown", attackCooldown);
        }
    }

    void AttackCooldown()
    {
        _animator.SetBool("Attack", false);
        isAttacked = false;
        
    }

    
}