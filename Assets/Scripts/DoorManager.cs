using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public Enemy commander1;
    public Enemy commander2;
    private void Update() {
        if (commander1.isDead && commander2.isDead)
        {
            this.gameObject.SetActive(false);
        }
    }
}
