using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AttackState : IState
{
    StateMachine AIFSM;
    float detectionRadius;
    float attackRadius;
    Transform transform;
    Transform weaponTransform;
    IAstarAI agent;
    Transform target;
    Weapon weapon;
    Vector3 dir;
    float nextFireTime;
    
    public AttackState(Enemy entity)
    {
        detectionRadius = entity.detectionRadius;
        attackRadius = entity.attackRadius;
        AIFSM = entity.AIFSM;
        transform = entity.transform;
        weaponTransform = entity.weaponTransform;
        agent = entity.agent;
        weapon = entity.weapon;
        target = entity.target.gameObject.transform;
    }
    public void Enter()
    {
        nextFireTime = 0;
       
    }
    public void Exit()
    {
        
    }
    public void Update()
    {
        dir = target.position - transform.position;
        
        if (dir.magnitude > detectionRadius)
        {
            AIFSM.Change("patrolling");
        }
        if (dir.magnitude > attackRadius && !agent.pathPending)
        {
            agent.destination = target.position;
            
            agent.SearchPath();
        }
    }
    public void FixedUpdate()
    {
        
        dir.Normalize();
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        weaponTransform.rotation = Quaternion.Euler(0f, 0f, angle - 90);
        if (weapon != null && Time.time > nextFireTime)
        {
            weapon.Fire();
            nextFireTime = Time.time + weapon.ROF;
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
}
