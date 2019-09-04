using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surront : MonoBehaviour
{
    public enum RotateSurront
    {
        RotateUp,
        RotateDown
    }
    public RotateSurront surront;

    public float rotationSpeed;


    // Update is called once per frame
    void Update()
    {
        if(surront == RotateSurront.RotateUp)
        {
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        }

        if(surront == RotateSurront.RotateDown)
        {
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime * -1);
        }
    }
}
