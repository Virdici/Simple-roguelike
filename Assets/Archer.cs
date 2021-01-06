using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Enemy
{
    private bool goodDistance;
    public GameObject arrow;
    public GameObject firingPlace;

    public override void Update()
    {
        FSM();
        if (currentDistance < 5)
        {
            goodDistance = false;
        }
        else
        {
            goodDistance = true;
        }
    }
    
    public override void FSM()
    {
        currentDistance = Vector3.Distance(transform.position, Player.transform.position);
        switch (state)
        {
            case EnemyState.idle:
                DoIdle();
                if (Player.CurrentRoomIndex == index && !goodDistance)
                {
                    state = EnemyState.reposition;
                }
                if (goodDistance && Player.CurrentRoomIndex == index)
                {
                    state = EnemyState.lightAttack;
                }

                break;
            case EnemyState.reposition:
                DoReposition();
                if (currentDistance >= 5)
                    state = EnemyState.idle;

                break;
            case EnemyState.lightAttack:
                DoLightAttack();
                if (currentDistance >= 5)
                {
                    state = EnemyState.idle;
                }
                else
                {
                    state = EnemyState.reposition;
                }

                break;
        }
    }

    protected override void DoIdle()
    {
        LookAtTarget();
        animator.SetBool("isRepositioning", false);
    }

    protected override void DoLightAttack()
    {
        LookAtTarget();
        animator.SetTrigger("attackLight");
    }

    protected override void StartAttack()
    {
        isAttacking = true;
        Agent.isStopped = true;
        isAnimationMovement = true;
    }
    protected override void StopAttack()
    {
        isAttacking = false;
        Agent.isStopped = false;
        isAnimationMovement = false;
    }
    public void ShootArrow()
    {
        var arrowObj = Instantiate(arrow, firingPlace.transform.position, Quaternion.identity);
        arrowObj.GetComponent<Rigidbody>().AddForce(transform.forward * 30, ForceMode.Impulse);
    }
}
