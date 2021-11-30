using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.UI;

public class Boss : Enemy
{
    public Slider slider;
    protected override void Awake()
    {
        AIFSM = new StateMachine();
        AIFSM.initialize();
        agent = GetComponent<IAstarAI>();
        rb = GetComponent<Rigidbody2D>();
        AIFSM.Add("laserBarrage", new LaserState(this));
        AIFSM.Add("singleFire", new TargetState(this));
        AIFSM.Add("dead", new DeadState());
    }
    protected override void Start()
    {
        AIFSM.Change("dead");
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        slider.value = health;
    }
}
