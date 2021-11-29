using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : Body
{
    protected override void Awake() {

        weapon = null;
    }
    public override void Fire() { }

    public override void Die()
    {
    }
}
