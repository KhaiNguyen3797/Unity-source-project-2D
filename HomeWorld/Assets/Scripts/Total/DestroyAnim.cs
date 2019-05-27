using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAnim : MonoBehaviour
{

    public void destroyAnim()
    {
        Destroy(gameObject);
    }

    public void enableSprite()
    {
        gameObject.SetActive(false);
    }
}
