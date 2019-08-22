using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public LayerMask collisionMask;
    public float speed;
    public float damage;
    float spWeight = .1f;

    void Start()
    {
        Collider[] initialCollisions = Physics.OverlapSphere(transform.position, 0.1f, collisionMask);
        if(initialCollisions.Length > 0)
        {
            OnhitObject(initialCollisions[0]);
        }
    }

    public void Setspeed(float newSpeed)
    {
        speed = newSpeed;
    }


    void Update()
    {
        float moveDistance = speed * Time.deltaTime;
        checkCollision(moveDistance);
        transform.Translate(Vector3.forward * moveDistance);
    }

    private void checkCollision(float moveDistance)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, moveDistance + spWeight, collisionMask, QueryTriggerInteraction.Collide))
        {
            OnhitObject(hit);
        }
    }

    private void OnhitObject(RaycastHit hit)
    {
        IDamageable idamage = hit.collider.GetComponent<IDamageable>();
        if(idamage != null)
        {
            idamage.TakeHit(damage, hit);
        }
        GameObject.Destroy(gameObject);
    }

    void OnhitObject(Collider col)
    {
        IDamageable idamage = col.GetComponent<IDamageable>();
        if (idamage != null)
        {
            idamage.TakeDamage(damage);
        }
        GameObject.Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
