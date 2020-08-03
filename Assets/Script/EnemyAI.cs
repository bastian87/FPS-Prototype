using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 5f;

    bool isProvoked;
    
    NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity;
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    
    void Update()
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        // We check if the enemy is provoked by shooting or hiting him.
        if (isProvoked)
        {
            EngageTarget();           
        }
        // We check the distance betwen the target and the enemy, if in range the enemy chase you.
        else if (distanceToTarget <= chaseRange)
        {
            isProvoked = true;            
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

    private void EngageTarget()
    {
        if(distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }

        if(distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }
    }

    private void ChaseTarget()
    {
        navMeshAgent.SetDestination(target.position);
    }

    private void AttackTarget()
    {
        Debug.Log("I am currently attacking you");
    }
}
