using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollision : MonoBehaviour
{
    [SerializeField]
    public Collider2D[] other;

    private void Awake()
    {
        for(int i= 0; i < other.Length; i++)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other[i], true);
        }
    }
}
