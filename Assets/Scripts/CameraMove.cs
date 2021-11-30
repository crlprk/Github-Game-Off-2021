using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraMove : MonoBehaviour
{
    public CinemachineVirtualCamera cam;
    CinemachineTrackedDolly dolly;
    public float speed;
    
    private void Awake() {
        dolly = cam.GetCinemachineComponent<CinemachineTrackedDolly>();
    }
    private void FixedUpdate() 
    {
        dolly.m_PathPosition += speed;
    }
}
