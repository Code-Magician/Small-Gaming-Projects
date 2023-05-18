using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    public static float buttonpressed = 0;

    public float Speed;

    List<Rigidbody2D> rbs;
    Vector3 force;

    void Start()
    {
        rbs = new List<Rigidbody2D>();
        if(GetComponent<Rigidbody2D>())
            rbs.Add(GetComponent<Rigidbody2D>());
        rbs.AddRange(GetComponentsInChildren<Rigidbody2D>());
    }

    void Update()
    {
        force = Speed * Vector3.right;
        if (Input.GetAxis("Horizontal") != 0)
            force *= Input.GetAxis("Horizontal");
        else if (buttonpressed != 0)
            force *= buttonpressed;
        else
            force = Vector3.zero;
        foreach(Rigidbody2D rb in rbs)
            rb.AddForce(force);
    }
}
