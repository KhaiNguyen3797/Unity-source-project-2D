using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opossum : MonoBehaviour
{
    public float runOpossum;
    public Transform player;
    public Transform checkGround;
    public Transform groundBack;
    public LayerMask whatIsGround;

    private bool col;
    private float groundRadius = .2f;
    private bool faceReturn;

    private Rigidbody2D r2;
    // Start is called before the first frame update
    void Start()
    {
        r2 = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        ReturnBackGround();
    }

    private void ReturnBackGround()
    {
        col = Physics2D.Linecast(player.position, checkGround.position, 1 << LayerMask.NameToLayer("Ground"));
        faceReturn = Physics2D.OverlapCircle(groundBack.position, groundRadius, whatIsGround);

        if(!col || faceReturn)
        {
            Vector3 temp = transform.localScale;
            if(temp.x == 1)
            {
                temp.x = -1;
            }else
            {
                temp.x = 1;
            }
            transform.localScale = temp;
        }
    }

    private void Move()
    {
        r2.velocity = new Vector2(transform.localScale.x ,0) * -runOpossum;
    }
}
