using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brute : Enemy
{
    public override void Start() {
        lightAttackPropability = 7;
    }
    public override void Update()
    {
        FSM();
        //Debug.Log(GetComponent<Health>().CurrentHP * 100 / GetComponent<Health>().HP);
        if (GetComponent<Health>().CurrentHP <= (40))
        {
            goodHealth = false;
        }
        else
        {
            goodHealth = true;
        }
        attackPropability = Random.Range(0, 10);
    }
    public override void FSM()
    {
        currentDistance = Vector3.Distance(transform.position, Player.transform.position);
        switch (state)
        {
            case EnemyState.idle:
                DoIdle();
                if (Player.CurrentRoomIndex == index && currentDistance > 2 && isAttacking == false)
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
                if (currentDistance >= 3 && currentDistance <= 4 && !goodHealth)
                {
                    state = EnemyState.special;
                }
                break;
            case EnemyState.lightAttack:
                DoLightAttack();
                if (currentDistance > 2)
                {
                    state = EnemyState.chase;
                }
                break;
            case EnemyState.strongAttack:
                DoStrongAttack();
                if (currentDistance > 2)
                {
                    state = EnemyState.chase;
                }
                break;
            case EnemyState.special: //jump attack
                DoSpecial();
                if (currentDistance > 2)
                {
                    state = EnemyState.chase;
                }
                break;
        }
    }

    protected override void DoIdle()
    {
        animator.SetBool("isWalking", false);
    }
    protected override void DoSpecial()
    {
        LookAtTarget();
        animator.SetBool("isWalking", false);
        animator.SetTrigger("attackJump");
    }

    protected override void StopAttack()
    {
        isAttacking = false;
        Agent.isStopped = false;
        animator.SetBool("isWalking", true);
        isAnimationMovement = false;
    }
}
