using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : MonoBehaviour
{
    public float flySpeed;
    public Transform flyStartPos;
    public Transform flyEndPos;

    private bool returnFly;
    private Rigidbody2D r2;
    // Start is called before the first frame update
    void Start()
    {
        r2 = GetComponent<Rigidbody2D>();
        transform.position = flyStartPos.position;
    }

    // Update is called once per frame
    void Update()
    {
        var dis = (transform.position - flyStartPos.position).sqrMagnitude;
        var disReturn = (transform.position - flyEndPos.position).sqrMagnitude;
        if(dis < 0.01)
        {
            returnFly = false;
        }
        if(disReturn < 0.01)
        {
            returnFly = true;
        }

        Flip();

        if (returnFly)
        {
            transform.position = Vector3.MoveTowards(r2.position, flyStartPos.position, flySpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(r2.position, flyEndPos.position, flySpeed * Time.deltaTime);
        }
    }

    private void Flip()
    {
        if ((flyEndPos.position.x < flyStartPos.position.x))
        {
            if (returnFly)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else
        {
            if (returnFly)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }
}
