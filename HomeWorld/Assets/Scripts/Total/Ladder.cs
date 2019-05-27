using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            player.ladder = true;
            player.ladderPosition.x = transform.position.x;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            player.ladder = false;
            player.climb = false;
            player.ladderPosition = Vector3.zero;
            player.grounded = false;
        }
    }
}
