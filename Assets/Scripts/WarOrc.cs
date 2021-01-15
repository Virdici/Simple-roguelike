using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarOrc : Enemy
{
    public override void Start()
    {
        lightAttackPropability = 5;
    }
    public override void Update()
    {
        FSM();
        if (GetComponent<Health>().CurrentHP <= 40)
        {
            goodHealth = false;
        }
        else
        {
            goodHealth = true;
        }
        attackPropability = Random.Range(1, 10);

    }
    public override void FSM()
    {
        currentDistance = Vector3.Distance(transform.position, Player.transform.position);
        switch (state)
        {
            case EnemyState.idle:
                DoIdle();
                if (Player.CurrentRoomIndex == index  && isAttacking == false)
                {
                    state = EnemyState.chase;
                }
                break;
            case EnemyState.chase:
                DoChase();
                if (currentDistance >= 1 && currentDistance <= 2)
                {
                    if (attackPropability <= lightAttackPropability)
                    {
                        state = EnemyState.lightAttack;
                    }
                    else
                    {
                        state = EnemyState.strongAttack;
                    }
                }
                if (!goodHealth && !didSpecial)
                {
                    state = EnemyState.retreat;
                }
                break;
            case EnemyState.lightAttack:
                DoLightAttack();
                if (currentDistance > 2 && isAttacking == false)
                {
                    state = EnemyState.idle;
                }
                if (!goodHealth && !didSpecial)
                {
                    state = EnemyState.retreat;
                }
                break;
            case EnemyState.strongAttack:
                DoStrongAttack();
                if (currentDistance > 2)
                {
                    state = EnemyState.idle;
                }
                if (!goodHealth && !didSpecial)
                {
                    state = EnemyState.retreat;
                }
                break;
            case EnemyState.retreat:
                DoRetreat();
                if (currentDistance >= 12)
                {
                    state = EnemyState.special;
                }
                break;
            case EnemyState.special: //heal
                DoSpecial();
                state = EnemyState.idle;
                break;
        }

        
    }

    protected override void DoChase()
    {
        animator.ResetTrigger("attackStrong");
        animator.ResetTrigger("attackLight");
        animator.SetBool("isWalking", true);
        LookAtTarget();
        Agent.SetDestination(PlayerMarker.transform.position);
    }
    protected override void DoRetreat()
    {
        LookAway();
        animator.SetBool("isWalking", false);
        animator.SetBool("isRetreating", true);
    }
    protected override void DoSpecial()
    {
        animator.SetBool("isRetreating", false);
      
        // animator.SetBool("isHealing", true);
        animator.SetTrigger("heal");
       
        didSpecial = true;

        
    }

    // protected override void DoSpecial()
    // {
    //     animator.SetBool("isRetreating", false);
    //     if (didSpecial == false)
    //     {
    //         animator.SetBool("isHealing", true);
    //     }
    //     didSpecial = true;

        
    // }
}
