using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstWeapon : Weapon
{
    public int burstCount;
    public override void Fire()
    {
        for (int i = 0; i < burstCount; i++)
        {
            GameObject bullet = ObjectPool.Instance.GetObject(ammunitionType);
            if (bullet != null)
            {
                bullet.transform.position = barrel.position + Vector3.forward;
                bullet.transform.rotation = transform.rotation * Quaternion.Euler(0, 0, Random.Range(-30, 30));
                bullet.SetActive(true);
            }
        }
    }
}
