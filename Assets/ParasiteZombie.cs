using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParasiteZombie : Enemy
{
  
    public override void Update()
    {
        FSM();
        if (GetComponent<Health>().CurrentHP <= 60)
        {
            goodHealth = false;
        }
        else
        {
            goodHealth = true;
        }

    }
    public override void FSM()  //naprawić randomowy atak, brak animacji wycofania i ragu
    {
        currentDistance = Vector3.Distance(transform.position, Player.transform.position);
        switch (state)
        {
            case EnemyState.idle:
                DoIdle();
                if (Player.CurrentRoomIndex == index && isAttacking == false)
                {
                    state = EnemyState.chase;
                }
                break;
            case EnemyState.chase:
                DoChase();
                if (currentDistance > 1 && currentDistance < 2)
                {
                    state = EnemyState.lightAttack;
                   
                }
                if (!goodHealth && !didSpecial)
                {
                    state = EnemyState.special;
                }
                break;
            case EnemyState.lightAttack:
                DoLightAttack();
                if (currentDistance > 2 && isAttacking == false)
                {
                    state = EnemyState.chase;
                }
                if (!goodHealth && !didSpecial)
                {
                    state = EnemyState.special;
                }
                break;
            case EnemyState.special: //scream
                DoSpecial();
                state = EnemyState.idle;
                break;
        }
    }

    protected override void DoIdle()
    {
        animator.SetBool("isWalking", false);
    }
    protected override void DoSpecial()
    {
        animator.SetTrigger("startRage");
        if (didSpecial == false)
        {
            animator.speed = 1.4f;
            Agent.speed = 2.4f;
        }
        didSpecial = true;
    }
}
