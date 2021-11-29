using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Centipede : MonoBehaviour
{
    public float distance = 1f;
    public float speed = 1f;
    public float turnSpeed = 1f;
    Vector2 dir = Vector2.up;
    float pathDist = 0f;
    public Path path;
    public List<Body> components;
    [HideInInspector]
    public Body leader => components[0];
    Rigidbody2D rb;
    bool isStart;
    float turnDir;
    int forwardMove;

    private void Awake() 
    {
        path = new Path(1);
        path.Add(this.transform.position, this.transform.rotation);
        rb = GetComponent<Rigidbody2D>();
        components = new List<Body>();
        isStart = true;
    }
    private void Update() 
    {
        turnDir = Input.GetAxis("Horizontal");
        forwardMove = Input.GetKey(KeyCode.W) ? 1 : 0;
        
    }
    private void FixedUpdate() 
    {
        if (turnDir != 0)
        {
            transform.Rotate(0, 0, (-turnSpeed * Time.deltaTime * turnDir * 100));
        }
        MoveLeader();
        MoveBody();
        DamageBody();
        if(Input.GetMouseButton(0))
        {
            FireBody();
        }   
        turnDir = 0;
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.layer != 7)
        {
            ContactPoint2D[] contacts = new ContactPoint2D[other.contactCount];
            other.GetContacts(contacts);
            Vector2 contactNormal = Vector2.zero;
            for (int i = 0; i < contacts.Length; i++)
            {    
                contactNormal += contacts[i].normal;
            }
            contactNormal /= contacts.Length;
            float angle = Mathf.Atan2(contactNormal.y, contactNormal.x) * Mathf.Rad2Deg - 90;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            turnDir = 0;
        }
        
    }

    public void AddBody(Body body)
    {
        body.Instantiate(components.Count);
        components.Add(body);
        body.MoveOnPath(path, 0f);
        if (path.capacity <= components.Count) {path.Resize();}
        components.Sort(CompareIndexOrder);
    }
    public void AddBody(Body body, int index)
    {
        if (isStart)
        {
            isStart = false;
        }
        index += 1;
        body.Instantiate(index);
        Debug.Log(index);
        UpdateBody(index, true);
        components.Add(body);
        body.MoveOnPath(path, 0f);
        if (path.capacity <= components.Count) {path.Resize();}
        components.Sort(CompareIndexOrder);
    }

    void MoveLeader()
    {
        rb.velocity = transform.up * speed * Time.deltaTime * 100 * forwardMove;
        Vector2 headToLeader = ((Vector2) leader.transform.position) - path.Head().pos;
        pathDist = headToLeader.magnitude;

        if (pathDist >= distance)
        {
            float leaderOvershoot = pathDist - distance;
            Vector2 pushDir = headToLeader.normalized * leaderOvershoot;
            path.Add(((Vector2) leader.transform.position) - pushDir, leader.transform.rotation); 
            pathDist = (((Vector2) leader.transform.position) - path.Head().pos).sqrMagnitude;
        }
    }

    void MoveBody()
    {
        float pathDistUnit = pathDist / distance;

        for (int i = 1; i < components.Count; i++)
        {
            Body body = components[i];

            body.MoveOnPath(path, pathDistUnit);
            Vector2 prevToNext = components[i - 1].transform.position - body.transform.position;
            float dist = prevToNext.magnitude;
            if (dist < distance)
            {
                float intersection = distance - dist;
                body.Push(-prevToNext.normalized * distance * intersection);
            }
        }
    }
    void DamageBody()
    {
        for (int i = 1; i < components.Count - 1; i++)
        {
            if (components[i].health <= 0)
            {
                Body body = components[i];
                UpdateBody(body.index, false);
                body.Die();
                components.Remove(body);
                path.Remove(body.index);
            }
        }
        if (components.Count == 2 && !isStart)
        {
            Debug.Log("Game over");
        }
    }

    void UpdateBody(int index, bool add)
    {
        for (int i = 0; i < components.Count; i++)
        {
            if (add)
            {
                if (components[i].index >= index)
                {
                    components[i].index += 1;
                }
            }
            else
            {
                if (components[i].index >= index)
                {
                    components[i].index -= 1;
                }
            }
            
        }
    }

    void FireBody()
    {
        for (int i = 1; i < components.Count; i++)
        {
            components[i].Fire();
        }
    }

    int CompareIndexOrder(Body a, Body b)
    {
        if (a.index < b.index) { return -1; }
        else if (a.index > b.index) { return 1; }
        return 0;
    }
}
