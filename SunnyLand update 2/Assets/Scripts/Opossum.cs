using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opossum : MonoBehaviour
{
    public Player player;
    [SerializeField] public float moveForce = 10f;
    public Rigidbody2D opossumR2;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        opossumR2 = gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {

    }
}
