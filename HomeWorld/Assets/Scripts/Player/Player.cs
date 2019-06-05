using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : character
{
    [SerializeField] public Vector3 ladderPosition;
    [SerializeField] public LayerMask whatIsGround;
    [SerializeField] public Collider2D croundCollider;
    [SerializeField] public Transform checkGround;
    [SerializeField] public Ghost ghost;
    [Range(0, 3)] [SerializeField] private float resetPlunge = 3;
    [SerializeField] public float climbSpeed;
    private const float groundRadius = 0.2f;
    private float timeLadder = 0.5f;
    private float forcePlunge;
    private int healthMax = 5;
    public float timeImmortal = 0.2f;
    public float knockback;
    public float knockbackLengt;
    public float knockbackCount;

    private bool gravityS;
    private bool jump;
    private bool crouch;
    private bool immortal;
    public bool grounded;
    public bool climb;
    public bool ladder;
    public bool knockFormRight;
    public int healthOur;

    private Rigidbody2D r2;
    private Animator anim;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        r2 = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        faceRight = true;
        healthOur = healthMax;
    }

    private void Update()
    {
        HandleInput();
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        CheckGround();
        Move(h, v);
    }

    #region CheckGround & CheckHealth

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

    public IEnumerator DamagePlayer()
    {
        if (!immortal)
        {
            healthOur -= 1;
            if (healthOur > 0)
            {
                immortal = true;
                yield return new WaitForSeconds(timeImmortal);
                immortal = false;
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    #endregion

    #region Move

    private void Move(float horizontal, float vertical)
    {
        if (knockbackCount <= 0)
        {
            r2.velocity = new Vector2(moveSpeed * horizontal, r2.velocity.y);
            anim.SetBool("Damage", false);
        }
        else
        {
            if (knockFormRight)
            {
                r2.velocity = new Vector2(-knockback, knockback);
                anim.SetBool("Damage", true);
            }
            else
            {
                r2.velocity = new Vector2(knockback, knockback);
                anim.SetBool("Damage", true);
            }
            knockbackCount -= Time.deltaTime;
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
            jumpForcePlayer();
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

    #endregion

    public void jumpForcePlayer()
    {
        r2.AddForce(new Vector2(0,jumpForce));
    }

    #region Collision

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Box"))
        {
            if(forcePlunge > resetPlunge)
            {
                Destroy(col.gameObject);
            }
        }

        if (col.collider.CompareTag("GroundPlat"))
        {
            transform.parent = col.transform;
        }

    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.collider.CompareTag("GroundPlat"))
        {
            transform.parent = null;
        }
    }
    #endregion

    #region Input
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

    #endregion
}
