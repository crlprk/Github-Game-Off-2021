using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
//Set seeker/A* stuff
//Change body layers to gfx and object seperatly
public class PatrolState : IState
{
    StateMachine AIFSM;
    float detectionRadius;
    Transform[] patrolPoints;
    Transform transform;
    bool isPursuit;
    IAstarAI agent;
    int index;
    public PatrolState(Enemy entity)
    {
        detectionRadius = entity.detectionRadius;
        AIFSM = entity.AIFSM;
        transform = entity.transform;
        patrolPoints = entity.patrolPoints;
        agent = entity.agent;
    }
    public void Enter()
    {
        isPursuit = false;
        index = 0;
        if (patrolPoints.Length != 0)
        {
            agent.destination = patrolPoints[0].position;
            agent.SearchPath();
        }
        
        
    }
    public void Exit()
    {
    }
    public void Update()
    {
        if (patrolPoints.Length != 0)
        {
            bool search = false;

            if (agent.reachedEndOfPath && !agent.pathPending)
            {
                index += 1;
                search = true;
            }
            index %= patrolPoints.Length;
            agent.destination = patrolPoints[index].position;

            
            if (search) agent.SearchPath();
            
        }
        if (isPursuit) AIFSM.Change("attacking");    
    }
    public void FixedUpdate()
    {
        if (OnTargetFound())
        {
            isPursuit = true;
        }
    }
    public void OnCollisionEnter()
    {

    }
    public void OnCollisionStay()
    {

    }
    public void OnCollisionExit()
    {

    }

    public bool OnTargetFound()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, detectionRadius, 3);
        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i].gameObject.GetComponentInParent<Body>().InChain)
            {
                Transform target = targets[i].transform;
                Vector2 dir = (target.position - transform.position);
                dir.Normalize();
                if (!Physics2D.Raycast(transform.position, dir, Vector2.Distance(transform.position, target.position), 7))
                {
                    return true;
                }
            }
        }
        return false;
    }
}
