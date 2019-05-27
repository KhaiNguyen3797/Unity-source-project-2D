using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crank : MonoBehaviour
{
    [SerializeField] public GameObject[] setCrank;

    public bool crankBool = false;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("crank", crankBool);
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < setCrank.Length; i++)
        {
            setCrank[i].gameObject.SetActive(crankBool);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            crankBool = !crankBool;
        }
    }
}
