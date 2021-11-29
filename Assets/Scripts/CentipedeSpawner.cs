using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentipedeSpawner : MonoBehaviour
{
    public Centipede centipede;
    public Body head;
    public Body tail;
    public List<Body> body;

    void Start()
    {
        centipede.AddBody(head);
        for (int i = 0; i < body.Count; i++)
        {
            centipede.AddBody(body[i]);
        }
        centipede.AddBody(tail);
    }
}
