using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] protected float moveSpeed = 7f;

    public Animator anim
    {
        get; private set;
    }

    [SerializeField]
    protected int heath;

    public bool attack { get; set; }

    [SerializeField] protected Transform bullerPos;
    [SerializeField] protected GameObject bullet;

    protected bool faceRight = true;
    // Start is called before the first frame update
    public virtual void Start()
    {
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeDirection()
    {
        faceRight = !faceRight;
        //transform.Rotate(0f, 180f, 0f);

        transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
    }

    public virtual void ThrowBuller(int value)
    {
        if (faceRight)
        {
            GameObject tmp = (GameObject)Instantiate(bullet, bullerPos.position, Quaternion.identity);
            tmp.GetComponent<Bullet>().Initialize(Vector2.right);
        }
        else
        {
            GameObject tmp = (GameObject)Instantiate(bullet, bullerPos.position, Quaternion.identity);
            tmp.GetComponent<Bullet>().Initialize(Vector2.left);
        }
    }
}
