using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralWeapon : Weapon
{
    public int spiralCount;
    public override void Fire()
    {
        float delta = 360 / spiralCount;
        float total = 0;
        if (ROF >= 0.1)
        {
            audioSource.PlayOneShot(audioSource.clip);
        }
        
        for (int i = 0; i < spiralCount; i++)
        {
            GameObject bullet = ObjectPool.Instance.GetObject(ammunitionType);
            if (bullet != null)
            {
                bullet.transform.position = barrel.position + Vector3.forward;
                bullet.transform.rotation = transform.rotation * Quaternion.Euler(0, 0, total);
                total += delta;
                bullet.SetActive(true);
            }
        }
    }
}
