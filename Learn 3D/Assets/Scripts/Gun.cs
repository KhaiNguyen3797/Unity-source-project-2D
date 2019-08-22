using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform muzzle;
    public Projectile bullet;
    public float msBetweenShoot;
    public float muzzleVelocity;
    public float timeShoot;


    public void Shoot()
    {

        if(Time.time > timeShoot)
        {
            timeShoot = Time.time + msBetweenShoot / 1000;
            Projectile newBullet = Instantiate(bullet, muzzle.position, muzzle.rotation) as Projectile;
            newBullet.Setspeed(muzzleVelocity);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
