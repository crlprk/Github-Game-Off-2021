using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardWeapon : Weapon
{
    public override void Fire()
    {
        GameObject bullet = ObjectPool.Instance.GetObject(ammunitionType);
        if (bullet != null)
        {
            bullet.transform.position = barrel.position + Vector3.forward;
            bullet.transform.rotation = transform.rotation;

            audioSource.PlayOneShot(audioSource.clip);
            
            bullet.SetActive(true);
        }
    }
}
