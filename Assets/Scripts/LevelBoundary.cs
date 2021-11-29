using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBoundary : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Projectile")
        {
            other.gameObject.SetActive(false);
        }
    }
}
