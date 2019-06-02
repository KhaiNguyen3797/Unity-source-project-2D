using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    public float forceX, forceY;
    public float timeDelay;
    public bool grounded;
    private bool jump;
    private bool faceRight = false;

    [SerializeField] public Transform checkGround;
    [SerializeField] public LayerMask whatIsGround;
    const float groundRadius = .2f;

    private Animator anim;
    private Rigidbody2D r2;

    // Start is called before the first frame update
    void Start()
    {
        r2 = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        grounded = false;
        Collider2D[] collider2D = Physics2D.OverlapCircleAll(checkGround.position, groundRadius, whatIsGround);

        for (int i = 0; i < collider2D.Length; i++)
        {
            if (collider2D[i].gameObject != gameObject)
            {
                anim.SetBool("FrogLand", false);
                grounded = true;
                anim.ResetTrigger("FrogJump");
            }
        }

        timeDelay -= Time.deltaTime;
        if(timeDelay <= 0)
        {
            jump = true;
        }

        if (grounded && jump)
        {
            jump = false;
            timeDelay = 2f;
            Debug.Log("Ten ten");
            if (faceRight)
            {
                Flip();
                anim.SetTrigger("FrogJump");
                jumpAttack(-forceX - forceX * 0.115f);
                timeDelay = 2f;
            }
            else
            {
                Flip();
                anim.SetTrigger("FrogJump");
                jumpAttack(forceX);
                                timeDelay = 2f;
            }
        }
        
        if(r2.velocity.y < 0)
        {
            anim.SetBool("FrogLand", true);
            anim.ResetTrigger("FrogJump");
        }
    }

    private void Flip()
    {
        faceRight = !faceRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void jumpAttack(float forceX)
    {
        r2.AddForce(new Vector2(r2.transform.position.x + forceX, r2.transform.position.y + forceY));
    }


}
