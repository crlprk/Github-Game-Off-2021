using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour, IDamagable
{
    public Transform target;
    public Weapon weapon;
    public Transform weaponTransform;
    public Transform[] patrolPoints;
    public float health;
    public float detectionRadius;
    public float attackRadius;
    public StateMachine AIFSM;
    [HideInInspector]
    public IAstarAI agent;

    public virtual void Awake()
    {
        AIFSM = new StateMachine();
        AIFSM.initialize();
        agent = GetComponent<IAstarAI>();
        AIFSM.Add("patrolling", new PatrolState(this));
        AIFSM.Add("attacking", new AttackState(this));

    }
    private void Start() {
        AIFSM.Change("patrolling");
    }
    // Update is called once per frame
    void Update()
    {
        AIFSM.m_currentState.Update();
        if (health <= 0)
        {
            Die();
        }
    }
    private void FixedUpdate() {
        AIFSM.m_currentState.FixedUpdate();
    }

    public void TakeDamage(Projectile source)
    {
        health -= source.damage;
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }
}
