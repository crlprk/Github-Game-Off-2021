using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour, IDamagable
{
    public SpriteRenderer spriteRenderer;
    public Sprite deathSprite;
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
    Rigidbody2D rb;
    public bool isDead;

    public virtual void Awake()
    {
        AIFSM = new StateMachine();
        AIFSM.initialize();
        agent = GetComponent<IAstarAI>();
        rb = GetComponent<Rigidbody2D>();
        AIFSM.Add("patrolling", new PatrolState(this));
        AIFSM.Add("attacking", new AttackState(this));
        AIFSM.Add("dead", new DeadState());


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
        spriteRenderer.sprite = deathSprite;
        gameObject.layer = 11;
        rb.mass = 0.01f;
        isDead = true;
        AIFSM.Change("dead");
    }
}
