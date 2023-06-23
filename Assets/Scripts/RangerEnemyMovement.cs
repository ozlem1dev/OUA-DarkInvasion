using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangerEnemyMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform[] waypoints;
    [SerializeField] public Transform player, base0;
    public LayerMask ground, player0;
    public Vector3 point;
    public float attackCooldown;
    public float sightRange, attackRange;
    public bool inSight, inRange;
    public float moveSpeed = 3f;
    public int attackDamage = 10;
    public Transform firePoint;
    public GameObject projectilePrefab;


    private int currentWaypointIndex = 0;
    private bool isAttacked;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        inSight = Physics.CheckSphere(transform.position, sightRange, player0);
        inRange = Physics.CheckSphere(transform.position, attackRange, player0);

        if (!inSight && !inRange)
        {
            Forward();
        }
        if (inSight && !inRange)
        {
            ChasePlayer();
        }
        if (inSight && inRange)
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


            if (Vector3.Distance(transform.position, waypoints[waypoints.Length - 1].position) <= 0.2f)
            {
                Destroy(gameObject);

                var baseHealth = base0.GetComponent<BaseHealth>();
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
        agent.SetDestination(player.position);
    }

    void Attack()
    {

        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!isAttacked)
        {
            GameObject sphere = Instantiate(projectilePrefab, transform.position, Quaternion.identity);


            Vector3 direction = player.transform.position - transform.position;
            direction.Normalize();


            Rigidbody sphereRigidbody = sphere.GetComponent<Rigidbody>();
            sphereRigidbody.velocity = direction * 10f;
            isAttacked = true;
            Invoke("AttackCooldown", attackCooldown);
        }


    }

    void AttackCooldown()
    {
        isAttacked = false;
    }

   

}
