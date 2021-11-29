using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LevelBoundary : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player")
        {
            vcam.m_Priority = 10;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Projectile")
        {
            other.gameObject.SetActive(false);
        }
        if (other.tag == "Player")
        {
            vcam.m_Priority = 0;
        }
    }
}
