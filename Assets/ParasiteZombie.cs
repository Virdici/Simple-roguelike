using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParasiteZombie : Enemy
{
    int screemChance;
    public BoxCollider Detector;
    public override void Update()
    {
        FSM();
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
                if (currentDistance > 0 && currentDistance < 2)
                {
                    state = EnemyState.lightAttack;
                }
                break;
            case EnemyState.lightAttack:
                DoLightAttack();
                    state = EnemyState.chase;

                break;
        }
    }

    protected override void DoIdle()
    {
        animator.SetBool("isWalking", false);
    }

    public void detectPlayer()
    {
    }

}
