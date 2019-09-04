using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoverment : MonoBehaviour
{
    public PlayerControler player;
    public float speed;
    public float h = 0;
    public bool jump = false;
    public bool runCircle = false;

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        h = Input.GetAxis("Horizontal") * speed;
        anim.SetFloat("Speed", Mathf.Abs(h));
        if(player.ground && Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }

        if(Input.GetKeyDown(KeyCode.L))
        {
            runCircle = true;
        }else if (Input.GetKeyUp(KeyCode.L))
        {
            runCircle = false;
        }

        if (runCircle)
        {
            anim.SetLayerWeight(1, 0);
            anim.SetBool("Roll", true);
        }
        else
        {
            anim.SetLayerWeight(1, 1);
            anim.SetBool("Roll", false);
        }

        if (player.checkJump)
        {
            anim.SetBool("Jump", true);
        }
        else
        {
            anim.SetBool("Jump", false);
        }

        anim.SetBool("Land", player.land);
    }


    public void setRunCircle()
    {
        runCircle = false;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        player.Move(jump, runCircle, h * Time.fixedDeltaTime);
        jump = false;
    }
}
