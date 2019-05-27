using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform door;
    public Player player;
    private bool teleportDoor;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    }

    private void Update()
    {
        if (teleportDoor)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                player.transform.position = door.transform.position;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            teleportDoor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            teleportDoor = false;
        }
    }
}
