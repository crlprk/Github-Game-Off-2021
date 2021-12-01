using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserWeapon : Weapon
{
    float nextSoundTime;
    protected override void Awake() {
        base.Awake();
        nextSoundTime = 0;
    }
    public override void Fire()
    {
        GameObject bullet = ObjectPool.Instance.GetObject(ammunitionType);
        if (bullet != null)
        {
            bullet.transform.position = barrel.position + Vector3.forward;
            bullet.transform.rotation = transform.rotation;
            if (Time.time > nextSoundTime)
            {
                audioSource.PlayOneShot(audioSource.clip);
                nextSoundTime = Time.time + 2;
            }
            
            
            bullet.SetActive(true);
        }
    }
}
