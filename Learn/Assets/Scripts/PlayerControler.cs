using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerControler : MonoBehaviour
{
    [SerializeField] public float jumpForce;
    [SerializeField] public float speed = 1f;
    [SerializeField] public float energy = 5f;
    [SerializeField] public float timeRecovery;
    [SerializeField]
    public float timeStartRecovery = 2f;
    [Range(0, 1)] [SerializeField] public float smoothTime = 0.05f;
    [SerializeField] public bool faceRight;
    [SerializeField] public bool ground;
    [SerializeField] public bool checkJump;
    [SerializeField] public bool land;
    [SerializeField] public bool energyRecovery;
    [SerializeField] public LayerMask whatIsGround;
    [SerializeField] public Transform checkGround;
    [SerializeField] public Transform BarUI;
    [SerializeField] public Transform TrailPlayer;

    float groundRadius = .2f;

    [Header("Event")]
    [Space]

    public UnityEvent OnRunCircleEvent;

    private Vector3 currentVelocity = Vector3.zero;
    public Rigidbody2D r2;

    // Start is called before the first frame update
    void Start()
    {
        r2 = GetComponent<Rigidbody2D>();
        faceRight = true;
        timeRecovery = timeStartRecovery;
        if(OnRunCircleEvent == null)
        {
            OnRunCircleEvent = new UnityEvent();
        }
    }

    private void FixedUpdate()
    {
        GroundCheck();
    }

    private void GroundCheck()
    {
        ground = false;

        if (!ground)
        {
            Collider2D[] collider2s = Physics2D.OverlapCircleAll(checkGround.position, groundRadius, whatIsGround);

            for (int i = 0; i < collider2s.Length; i++)
            {
                if (collider2s[i].gameObject != gameObject)
                {
                    ground = true;
                    land = false;
                    checkJump = false;
                }
            }
        }
    }

    public void Move(bool jump, bool runCircle, float move)
    {
        if (runCircle)
        {
            TrailPlayer.GetComponent<TrailRenderer>().enabled = true;
            speed = 4f;
            r2.gravityScale = 4f;
            energy -= Time.deltaTime;
            timeRecovery = timeStartRecovery;
            if (energy <= 0)
            {
                energy = 0;
                runCircle = false;
                OnRunCircleEvent.Invoke();
            }
        }
        else
        {
            TrailPlayer.GetComponent<TrailRenderer>().enabled = false;
            speed = 1f;
            r2.gravityScale = 2f;
            if (energy < 5)
            {
                energyRecovery = true;
            }


            if (energyRecovery)
            {
                timeRecovery -= Time.deltaTime;
                if (timeRecovery <= 0)
                {
                    energy += Time.deltaTime;
                    if (energy >= 5)
                    {
                        energy = 5;
                        energyRecovery = false;
                    }
                }
            }
            else
            {
                timeRecovery = timeStartRecovery;
            }
        }

        BarUI.localScale = new Vector3(energy / 5, 1, 1);

        Vector3 targetVelocity = new Vector2(move * 10f * speed, r2.velocity.y);

        r2.velocity = Vector3.SmoothDamp(r2.velocity, targetVelocity, ref currentVelocity, smoothTime);


        if (ground && jump)
        {
            jump = false;
            checkJump = true;
            r2.AddForce(new Vector2(0f, jumpForce));
        }

        if (!ground && r2.velocity.y < 0)
        {
            land = true;
        }

        if (move > 0 && !faceRight)
        {
            Flip();
        }
        else if (move < 0 && faceRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        faceRight = !faceRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
