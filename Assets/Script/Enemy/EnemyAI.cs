using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Jobs;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 5f;
    [SerializeField] float turnSpeed = 5f;

    bool isProvoked;
    Animator anim;
    NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity;
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
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
        FaceTarget();
        if (distanceToTarget >= navMeshAgent.stoppingDistance)
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
        anim.SetBool("isAttack", false);
        anim.SetTrigger("Move");
        navMeshAgent.SetDestination(target.position);
    }
    private void AttackTarget()
    {        
        anim.SetBool("isAttack", true);
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }
}
