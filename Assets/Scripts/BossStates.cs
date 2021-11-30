using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class LaserState : AttackState
{
    Weapon laser;
    Transform[] patrolPoints;
    int index;
    float stateEntered;

    public override void Enter()
    {
        index = 0;
        stateEntered = Time.time;
        if (patrolPoints.Length != 0)
        {
            agent.destination = patrolPoints[0].position;
            agent.SearchPath();
        }
    }
    public LaserState(Boss entity) : base(entity) 
    {
        laser = entity.weapon[0];
        patrolPoints = entity.patrolPoints;
    }

    public override void Update()
    {
        dir = target.position - transform.position;
        bool search = false;

        if (agent.reachedEndOfPath && !agent.pathPending)
        {
            index += 1;
            search = true;
        }
        index %= patrolPoints.Length;
        agent.destination = patrolPoints[index].position;

        
        if (search) agent.SearchPath();
        if (Time.time > stateEntered + 5)
        {
            AIFSM.Change("singleFire");
        }
    }

    public override void FixedUpdate()
    {
        dir.Normalize();
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        weaponTransform.rotation = Quaternion.Euler(0f, 0f, angle - 90);
        if (laser != null && Time.time > nextFireTime)
        {
            laser.Fire();
            nextFireTime = Time.time + laser.ROF;
        }
    }
}

public class TargetState : AttackState
{
    Weapon[] weapons;
    Transform[] patrolPoints;
    float stateEntered;
    float[] nextFireTimes;

    public override void Enter()
    {
        stateEntered = Time.time;
    }
    public TargetState(Boss entity) : base(entity) 
    { 
        weapons = entity.weapon;
        nextFireTimes = new float[weapons.Length-1];
        for (int i = 0; i < nextFireTimes.Length; i++)
        {
            nextFireTimes[i] = 0;
        }
    }

    public override void Update()
    {
        dir = target.position - transform.position;

        if (dir.magnitude > detectionRadius)
        {
            AIFSM.Change("laserBarrage");
        }
        if (dir.magnitude > attackRadius && !agent.pathPending)
        {
            agent.destination = target.position;
            
            agent.SearchPath();
        }
        if (Time.time > stateEntered + 15)
        {
            AIFSM.Change("laserBarrage");
        }
    }

    public override void FixedUpdate()
    {
        dir.Normalize();
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        weaponTransform.rotation = Quaternion.Euler(0f, 0f, angle - 90);
        for (int i = 1; i < weapons.Length; i++)
        {
            if (Time.time > nextFireTimes[i-1])
            {
                weapons[i].Fire();
                nextFireTimes[i-1] = Time.time + weapons[i].ROF;
            }
        }
        
    }
}