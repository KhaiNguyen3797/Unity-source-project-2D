using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector3 velocity;
    private Rigidbody r2;
    // Start is called before the first frame update
    void Start()
    {
        r2 = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        r2.MovePosition(r2.position + velocity * Time.fixedDeltaTime);
    }

    public void Move(Vector3 moveVelocity)
    {
        velocity = moveVelocity;
    }

    public void LookAt(Vector3 point)
    {
        Vector3 hei = new Vector3(point.x, transform.position.y, point.z);
        transform.LookAt(hei);
    }
}
