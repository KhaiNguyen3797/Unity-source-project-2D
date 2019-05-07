using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Character
{
    private static Player instance;
    public static Player Instance
    {
        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<Player>();
            }
            return instance;    
        }
    }

    [SerializeField] public float jumpForce = 700f;
    [SerializeField] public Collider2D crouchDisable;
    [SerializeField] public Transform[] groundCheck;
    [SerializeField] public LayerMask whatisGround;
    [SerializeField] public GameObject Bum;
    const float groundRadius = .2f;

    public Rigidbody2D r2 { get; set; }
    public bool grounded { get; set; }
    public bool crouched { get; set; }
    public bool jumping { get; set; }


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        r2 = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HandleInput();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        bool wasground = grounded;
        grounded = CheckGround(wasground);
        HandleMovement(h);
        OnJumpWeight();
        Flip(h);
        jumping = false;
    }

    public void HandleMovement(float horizontal)
    {
        if (r2.velocity.y < 0)
        {
            anim.SetBool("Landing", true);
        }

        r2.velocity = new Vector2(horizontal * moveSpeed, r2.velocity.y);

        anim.SetFloat("Speed", Mathf.Abs(horizontal));

        if(grounded && jumping)
        {
            grounded = false;
            jumping = false;
            r2.AddForce(new Vector2(0, jumpForce));
            anim.SetTrigger("Jumping");
        }
        //Khi crouch đúng thì tắt collider trên đầu và player đứng im
        if (crouched && grounded)
        {
            if(crouchDisable != null)
            {
                crouchDisable.enabled = false;
            }
            r2.velocity = Vector3.zero;
            anim.SetBool("Crouching", true);
        }
        else if(crouched == false)
        {
            if (crouchDisable != null)
            {
                crouchDisable.enabled = true;
            }
            anim.SetBool("Crouching", false);
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumping = true;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            crouched = true;
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            crouched = false;
        }
    }

    private bool CheckGround(bool wasground)
    {
        if(r2.velocity.y <= 0)
        {
            foreach(Transform point in groundCheck)
            {
                Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(point.position, groundRadius, whatisGround);

                for(int i = 0; i < collider2Ds.Length; i++)
                {
                    if(collider2Ds[i].gameObject != gameObject)
                    {
                        anim.ResetTrigger("Jumping");
                        anim.SetBool("Landing", false);
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private void Flip(float horizontal)
    {
        if((horizontal > 0 && !faceRight) || (horizontal < 0 && faceRight))
        {
            ChangeDirection();  
        }
    }

    private void OnJumpWeight()
    {
        if (!grounded)
        {
            anim.SetLayerWeight(1, 1);
        }
        else
        {
            anim.SetLayerWeight(1, 0);
        }
    }


    //Kiểm tra góc khi player va trạm enemy
    private void OnCollisionEnter2D(Collision2D target)
    {
        Enemy enemy = target.collider.GetComponent<Enemy>();
        if(enemy != null)
        {
            foreach (ContactPoint2D point in target.contacts)
            {
                //Debug.DrawLine(point.point, point.point + point.normal, Color.red, 10);
                //Debug.Log(point.normal);
                if(point.normal.y >= 0.9f)
                {
                    enemy.Damage();
                    r2.velocity = Vector2.up * 10;
                }
                else
                {
                    Damage();
                }
            }
        }

        Bullet bullet = target.collider.GetComponent<Bullet>();
        if(bullet != null)
        {
            Damage();
        }
    }
    
    private void Damage()
    {
        heath -= 1;
        anim.SetTrigger("DamageHurt");
        r2.velocity = Vector2.up * 10;
    }
}
