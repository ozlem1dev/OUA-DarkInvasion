using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;

public class MeleeEnemyMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform[] waypoints;
    [SerializeField] public Transform player;
    public LayerMask ground, player0;
    public Vector3 point;
    public float attackCooldown;
    public float sightRange, attackRange;
    public bool inSight, inRange;
    public float moveSpeed = 3f;
    public int attackDamage=10;


    private int currentWaypointIndex = 0;
    private bool isAttacked;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();  
    }

    private void Update()
    {
        inSight = Physics.CheckSphere(transform.position, sightRange, player0);
        inRange=Physics.CheckSphere(transform.position,attackRange, player0);

        if(!inSight && !inRange)
        {
            Forward();
        }
        if(inSight && !inRange)
        {
            ChasePlayer();
        }
        if(inSight && inRange)
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

            transform.position += direction * moveSpeed * Time.deltaTime;

            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            targetRotation.eulerAngles = new Vector3(0f, targetRotation.eulerAngles.y, 0f);
            transform.rotation = targetRotation;



            if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) <= 0.1f)
            {
                
                currentWaypointIndex++;
                currentWaypointIndex = Mathf.Clamp(currentWaypointIndex, 0, waypoints.Length - 1);

            }
        }
    }


    void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    void Attack()
    {
        var playerHealth = player.GetComponent<CharacterHealth>();
        agent.SetDestination(transform.position);
        transform.LookAt(player);
        
        if (!isAttacked && playerHealth != null)
        {
            Debug.Log("ATTACK");
            playerHealth.takeDamage(attackDamage);
            
            isAttacked = true;
            Invoke("AttackCooldown", attackCooldown);
        }
        

    }

    void AttackCooldown()
    {
        isAttacked = false;
    }

}

