using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float damage;
    Rigidbody2D rb;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        rb.velocity = transform.up * speed;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        IDamagable target = other.gameObject.GetComponent<IDamagable>();
        if (target != null)
        {
            target.TakeDamage(this);
            Debug.Log("Damaged");
        }
        this.gameObject.SetActive(false);
    }
}
