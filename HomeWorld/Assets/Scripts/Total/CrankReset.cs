using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrankReset : MonoBehaviour
{
    public Crank crank;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            crank.crankBool = !crank.crankBool;
            anim.SetBool("crank", !crank.crankBool);
        }
    }
}
