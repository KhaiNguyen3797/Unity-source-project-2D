using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour, IUseable
{
    [SerializeField]
    public Collider2D platforms;
    public void Use()
    {
        if (Player.Instance.Onladder)
        {
            //Stop Landding
            UseLadder(false, 3, 0,1);
        }
        else
        {
            //Start Landding
            UseLadder(true, 0, 2, 0);
            Physics2D.IgnoreCollision(Player.Instance.GetComponent<Collider2D>(), platforms, true);
        }
    }


    private void UseLadder(bool Onladder, int gravity, int layerWight, int animSpeed)
    {
        Player.Instance.Onladder = Onladder;
        Player.Instance.r2.gravityScale = gravity;
        Player.Instance.anim.SetLayerWeight(2, layerWight);
        Player.Instance.anim.speed = animSpeed;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            UseLadder(false, 3, 0,1);
        }
    }

}
