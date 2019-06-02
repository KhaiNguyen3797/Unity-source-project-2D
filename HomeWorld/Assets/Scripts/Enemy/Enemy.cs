using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Player player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }


    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Player"))
        {
            player.knockbackCount = player.knockbackLengt;
            if(col.transform.position.x < transform.position.x)
            {
                player.knockFormRight = true;
            }
            else
            {
                player.knockFormRight = false;
            }
        }   
    }
}
