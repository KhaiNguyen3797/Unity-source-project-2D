using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class character : MonoBehaviour
{
    [SerializeField] public float moveSpeed;
    [SerializeField] public float jumpForce;
    [SerializeField] public bool faceRight;
    // Start is called before the first frame update
    public virtual void Start()
    {
        faceRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Flip()
    {
        faceRight = !faceRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
