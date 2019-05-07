using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformsCollider : MonoBehaviour
{
    private BoxCollider2D playerCollider;
    private CircleCollider2D playerColliderCircle;

    [SerializeField]
    public BoxCollider2D platformsCollider;
    [SerializeField]
    public BoxCollider2D platformsTrigger;
    // Start is called before the first frame update
    void Start()
    {
        playerCollider = GameObject.Find("Player").GetComponent <BoxCollider2D>();
        playerColliderCircle = GameObject.Find("Player").GetComponent<CircleCollider2D>();
        Physics2D.IgnoreCollision(platformsCollider, platformsTrigger, true);
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(platformsCollider, playerColliderCircle, true);
            Physics2D.IgnoreCollision(platformsCollider, playerCollider, true);
        }
    }

    private void OnTriggerExit2D(Collider2D target)
    {
        if (target.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(platformsCollider, playerColliderCircle, false);
            Physics2D.IgnoreCollision(platformsCollider, playerCollider, false);
        }
    }
}
