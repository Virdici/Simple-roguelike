using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mutant : Enemy
{
    public override void Start() {
        lightAttackPropability = 0;
    }
    public override void Update()
    {
        FSM();
        if (GetComponent<Health>().CurrentHP <= 50)
        {
            goodHealth = false;
        }
        else
        {
            goodHealth = true;
        }
        attackPropability = Random.Range(0, 10);

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
            case EnemyState.strongAttack:
                DoStrongAttack();
                if (currentDistance > 2)
                {
                    state = EnemyState.chase;
                }
                if (!goodHealth && !didSpecial)
                {
                    state = EnemyState.special;
                }
                break;
            case EnemyState.special: //rage
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
            animator.speed = 2.2f;
            Agent.speed = 3.5f;
        }
        didSpecial = true;
    }

    protected override void DoChase()
    {
        animator.SetBool("isWalking", true);
        animator.ResetTrigger("attackLight");
        animator.ResetTrigger("attackStrong");
        LookAtTarget();
        Agent.SetDestination(PlayerMarker.transform.position);
    }

}
