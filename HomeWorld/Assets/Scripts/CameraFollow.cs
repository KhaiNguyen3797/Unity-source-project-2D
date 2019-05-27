using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float minX, maxX;
    public float minY, maxY;

    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(player.transform.position.x, minX, maxX), Mathf.Clamp(player.transform.position.y, minY, maxY), transform.position.z);
    }
}
