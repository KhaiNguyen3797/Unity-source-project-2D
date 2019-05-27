using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    public GameObject animItem;

    public void Instan()
    {
        Instantiate(animItem, transform.position, transform.rotation);
    }
}
