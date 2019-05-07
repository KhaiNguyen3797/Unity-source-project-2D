using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public IEnemyState currentState;

    public GameObject Target { get; set; }

    [SerializeField]
    public GameObject Bum;

    public Rigidbody2D r2;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        r2 = GetComponent<Rigidbody2D>();
        ChangeState(new IdleState());   
    }

    // Update is called once per frame
    void Update()
    {
        currentState.Execute();

        LookAtTarget();
    }


    private void LookAtTarget()
    {
        if (Target != null)
        {
            float xDir = Target.transform.position.x - transform.position.x;
            if (xDir < 0 && faceRight || xDir > 0 && !faceRight)
            {
                ChangeDirection();
            }
        }
    }


    public void ChangeState(IEnemyState newState)
    {
        if(currentState != null)
        {
            currentState.Exit();
        }
        currentState = newState;

        currentState.Enter(this);
    }

    public void Move()
    {
        if (!attack)
        {
            anim.SetFloat("Speed", 1);
            transform.Translate(GetDirection() * (moveSpeed * Time.deltaTime));
        }
    }

    public Vector2 GetDirection()
    {
        return faceRight ? Vector2.right : Vector2.left;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        currentState.OnTriggerEnter(other);
    }

    public void Damage()
    {
        heath -= 1;
        if (heath <= 0)
        {
            anim.SetTrigger("Hurt");
            Destroy(gameObject);
            Instantiate(Bum, r2.transform.position, Quaternion.identity);
        }
    }
}
