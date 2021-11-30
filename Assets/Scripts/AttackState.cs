using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AttackState : IState
{
    protected StateMachine AIFSM;
    protected float detectionRadius;
    protected float attackRadius;
    protected Transform transform;
    protected Transform weaponTransform;
    protected IAstarAI agent;
    protected Transform target;
    Weapon weapon;
    protected Vector3 dir;
    protected float nextFireTime;
    
    public AttackState(Enemy entity)
    {
        detectionRadius = entity.detectionRadius;
        attackRadius = entity.attackRadius;
        AIFSM = entity.AIFSM;
        transform = entity.transform;
        weaponTransform = entity.weaponTransform;
        agent = entity.agent;
        weapon = entity.weapon[0];
        target = entity.target.gameObject.transform;
    }
    public virtual void Enter()
    {
        nextFireTime = 0;
       
    }
    public virtual void Exit()
    {
        
    }
    public virtual void Update()
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
    public virtual void FixedUpdate()
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
