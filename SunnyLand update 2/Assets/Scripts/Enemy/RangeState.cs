using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeState : IEnemyState
{

    private Enemy enemy;

    private float throwTimer;
    private float throwCoolDown = 2f;
    private bool canthrow = true;

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        ThrowAttack();
        if(enemy.Target != null)
        {
            enemy.Move();
        }
        else
        {
            enemy.ChangeState(new IdleState());
        }
    }

    public void Exit()
    {

    }

    public void OnTriggerEnter(Collider2D other)
    {

    }

    private void ThrowAttack()
    {
        throwTimer += Time.deltaTime;

        if(throwTimer >= throwCoolDown)
        {
            canthrow = true;
            throwTimer = 0;
        }

        if (canthrow)
        {
            canthrow = false;
            enemy.anim.SetTrigger("Throw");
        }
    }
}
