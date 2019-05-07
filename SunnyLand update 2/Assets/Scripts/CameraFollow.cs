using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] public float xmax;
    [SerializeField] public float ymax;

    [SerializeField] public float xmin;
    [SerializeField] public float ymin;

    private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(target.position.x, xmin, xmax), Mathf.Clamp(target.position.y, ymin, ymax), transform.position.z);
    }
}
