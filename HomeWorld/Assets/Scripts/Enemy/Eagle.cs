using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : MonoBehaviour
{
    [SerializeField] public Transform pointFly;
    [SerializeField] public Transform pointFly2;
    [SerializeField] public float speedFly;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = pointFly.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, pointFly2.transform.position, speedFly*Time.deltaTime);
    }
}
