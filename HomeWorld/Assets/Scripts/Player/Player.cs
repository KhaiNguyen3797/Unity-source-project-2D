using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : character
{
    [SerializeField] public LayerMask whatIsGround;
    [SerializeField] public Collider2D croundCollider;
    [SerializeField] public Transform checkGround;
    [SerializeField] public Vector3 ladderPosition;
    [SerializeField] public Transform smoke;
    [Range(0,3)] [SerializeField] private float resetPlunge = 3;
    [SerializeField] public float climbSpeed;
    [SerializeField] public Ghost ghost;
    private const float groundRadius = 0.2f;
    private float timeLadder = 0.5f;
    private float forcePlunge;

    private bool gravityS;
    private bool jump;
    private bool crouch;
    public bool grounded;
    public bool climb;
    public bool ladder;


    private Rigidbody2D r2;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        r2 = GetComponent<Rigidbody2D>();
        faceRight = true;
        //smoke.transform.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        CheckGround();
        Move(h, v);
    }

    private void CheckGround()
    {
        Collider2D[] physics2D = Physics2D.OverlapCircleAll(checkGround.position, groundRadius, whatIsGround);
        for (int i = 0; i < physics2D.Length; i++)
        {
            if (physics2D[i].gameObject != gameObject)
            {
                grounded = true;
                gravityS = false;
                r2.gravityScale = 5;
                forcePlunge = 0;
                ghost.makeGhost = false;
                anim.ResetTrigger("Jump");
                anim.SetBool("Land", false);
            }
        }
    }

    private void Update()
    {
        HandleInput();
    }

    private void Move(float horizontal, float vertical)
    {
        r2.velocity = new Vector2(moveSpeed * horizontal, r2.velocity.y);
        if((horizontal > 0 || horizontal < 0) && grounded && !crouch)
        {
            smoke.transform.gameObject.SetActive(true);
        }
        else if(horizontal == 0 || !grounded)
        {
            smoke.transform.gameObject.SetActive(false);
        }
        anim.SetFloat("Speed", Mathf.Abs(horizontal));

        if (r2.velocity.y < 0)
        {
            anim.SetBool("Land", true);
        }

        if (climb && ladder)
        {
            r2.gravityScale = 0;
            grounded = false;
            anim.SetBool("Climb", true);
            timeLadder -= Time.deltaTime;
            if (timeLadder <= 0)
            {
                if (horizontal > 0 || horizontal < 0)
                {
                    climb = false;
                    r2.gravityScale = 5;
                    r2.AddForce(new Vector2(0, jumpForce/2));
                }
                timeLadder = 0.5f;
            }
            r2.velocity = new Vector2(0, climbSpeed * vertical);
            if(vertical == 0 && ladder && climb)
            {
                anim.speed = 0;
            }
            else
            {
                anim.speed = 1;
            }
            transform.position = new Vector3(ladderPosition.x, transform.position.y, transform.position.z);
        }
        if(!climb)
        {
            anim.SetBool("Climb", false);
            if (!gravityS)
            {
                r2.gravityScale = 5f;
                ghost.makeGhost = false;
            }
            else
            {
                r2.gravityScale = 20f;
                forcePlunge += Time.deltaTime;
                ghost.makeGhost = true;
            }
        }

        if (horizontal > 0 && !faceRight)
        {
            Flip();
            crouch = false;
        }
        else if (horizontal < 0 && faceRight)
        {
            Flip();
            crouch = false;
        }

        if(jump && grounded)
        {
            climb = false;
            anim.SetTrigger("Jump");
            jump = false;
            grounded = false;
            r2.AddForce(new Vector2(0, jumpForce));
        }

        if(crouch && grounded)
        {
            anim.SetBool("Crouch", true);
            moveSpeed = 0;
            if(croundCollider != null)
            {
                croundCollider.enabled = false;
            }
        }
        else
        {
            anim.SetBool("Crouch", false);
            moveSpeed = 10f;
            if (croundCollider != null)
            {
                croundCollider.enabled = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Box"))
        {
            if(forcePlunge > resetPlunge)
            {
                Destroy(col.gameObject);
            }
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            jump = true;
        }

        if (Input.GetKeyDown(KeyCode.S) && grounded)
        {
            crouch = true;
        }else if (Input.GetKeyUp(KeyCode.S) && grounded)
        {
            crouch = false;
        }

        if (Input.GetKeyDown(KeyCode.W) && ladder)
        {
            climb = true;
        }

        if(Input.GetKeyDown(KeyCode.S) && !grounded && !ladder)
        {
            gravityS = true;
        }
    }
}
