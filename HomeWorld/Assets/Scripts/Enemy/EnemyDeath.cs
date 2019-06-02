using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public GameObject enemyDeath;
    public float bounceOnEnemy;
    private Rigidbody2D r2;
    private void Start()
    {
        r2 = transform.parent.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.isTrigger == false)
        {
            if (col.CompareTag("Enemy"))
            {
                Destroy(col.gameObject);
                r2.velocity = new Vector2(r2.velocity.x, bounceOnEnemy);
                Instantiate(enemyDeath, col.transform.position, col.transform.rotation);
            }
        }
    }
}
