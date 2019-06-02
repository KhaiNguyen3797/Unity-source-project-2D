using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollCamera : MonoBehaviour
{
    public Player player;
    public float timeDelay = 1;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 offset = GetComponent<MeshRenderer>().material.mainTextureOffset;
        offset.x = player.transform.position.x;
        GetComponent<MeshRenderer>().material.mainTextureOffset = offset * Time.deltaTime * timeDelay;
    }
}