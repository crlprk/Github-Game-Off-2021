using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour, IDamagable
{
    public Centipede centipede;
    public Animator animator;
    public int index;
    public float health;
    [SerializeField]
    bool isDragged;
    [SerializeField]
    bool inChain;
    public bool InChain { get { return inChain; }}  
    bool isDead;
    Vector3 mouseStartPos;
    Vector3 bodyStartPos;
    public Weapon weapon;
    float nextFireTime;
    Rigidbody2D rb;
    //Body connected;

    protected virtual void Awake() {
        inChain = false;
        isDead = false;
        isDragged = false;
        nextFireTime = 0;
        GetComponent<Collider2D>().enabled = false;
        rb = GetComponent<Rigidbody2D>();
    }
    public virtual void Instantiate(int index)
    {
        this.index = index;
        inChain = true;
        if (this.tag == "Body")
        {
            transform.GetChild(2).gameObject.SetActive(false);
            animator.SetBool("InChain", true);
            GetComponent<Collider2D>().enabled = true;
        }
        
        
        //connected = centipede.components[index];
    }

    public void MoveOnPath(Path path, float dist)
    {
        Point prev = path.Head(-index);
        Point next = path.Head(-index + 1);

        //transform.position = Vector2.MoveTowards(transform.position, next.pos, dist);
        //Vector2 target = Vector2.Lerp(prev.pos, next.pos, dist);
        transform.position = Vector2.Lerp(prev.pos, next.pos, dist);
        //Debug.Log(Vector2.Lerp(prev.pos, next.pos, dist));
       // Vector2 target = Vector2.Lerp(prev.pos, next.pos, dist) - (Vector2) transform.position;
        //rb.MovePosition(target);
        transform.rotation = Quaternion.Lerp(prev.rot, next.rot, dist);
    }

    public virtual void Fire() 
    {
        if (weapon != null && Time.time > nextFireTime)
        {
            weapon.Fire();
            nextFireTime = Time.time + weapon.ROF;
        }
        
    }
    public virtual void TakeDamage(Projectile source)
    {
        if (inChain)
        {
            health -= source.damage;
        }
        
    }
    public virtual void Die()
    {
        animator.SetBool("IsDead", true);
        isDead = true;
        inChain = false;
        gameObject.layer = 11;
    }

    public void Push(Vector2 dir)
    {
        transform.position += (Vector3) dir;
    }

    protected void OnTriggerEnter2D(Collider2D other) 
    {
        if (inChain)
        {
            Body target = null;
            if (other.tag == "Body")
            {
                target = other.gameObject.GetComponentInParent<Body>();
                if (target.isDragged && !target.inChain)
                {
                    centipede.AddBody(target, index);
                    //connected = target;
                    Debug.Log("Added");
                }
            }
        }
    }

    protected void OnMouseDown() {
        if (!inChain && !isDead)
        {
            isDragged = true;
            mouseStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            bodyStartPos = transform.localPosition;
        }
    }

    protected void OnMouseDrag() {
        if(isDragged)
        {
            transform.localPosition = bodyStartPos + (Camera.main.ScreenToWorldPoint(Input.mousePosition) - mouseStartPos);
        }
    }

    protected void OnMouseUp() {
        isDragged = false;
    }
}
