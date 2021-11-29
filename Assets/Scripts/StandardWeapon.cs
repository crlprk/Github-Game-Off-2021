using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardWeapon : Weapon
{
    public string ammunitionType;
    public override void Fire()
    {
        GameObject bullet = ObjectPool.Instance.GetObject(ammunitionType);
        if (bullet != null)
        {
            bullet.transform.position = transform.position + Vector3.forward;
            bullet.transform.rotation = transform.rotation;
            bullet.SetActive(true);
        }
    }
}
