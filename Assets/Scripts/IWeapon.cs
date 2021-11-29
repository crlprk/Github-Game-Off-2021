using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public float ROF;
    public virtual void Fire() { }
    public bool isEquipped;

    protected virtual void FixedUpdate() 
    {
        if (isEquipped)
        {
            if (GetComponentInParent<Body>().InChain)
            {
                Vector3 mousePos = Input.mousePosition;
                Vector3 objPos = Camera.main.WorldToScreenPoint(transform.position);
                mousePos.x -= objPos.x;
                mousePos.y -= objPos.y;
                float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg - 90;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            }
        }
    }
    
}
