using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private GameObject destination;
    private NavMeshAgent agent;

    private void Start()
    {
        agent= GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        agent.SetDestination(destination.transform.position);
    }
}

