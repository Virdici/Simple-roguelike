using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MawJLaygo : Enemy
{
    private bool goodDistance;
    public GameObject fireball;
    public GameObject fireArea;
    public GameObject firingPlace;

    public override void Start() {
        lightAttackPropability = 1;
    }
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
        attackPropability = Random.Range(0, 10);

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
                    if (attackPropability <= lightAttackPropability)
                    {
                        state = EnemyState.lightAttack;
                    }
                    else
                    {
                        state = EnemyState.strongAttack;
                    }
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
            case EnemyState.strongAttack:
                DoStrongAttack();
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

    protected override void DoStrongAttack()
    {
        LookAtTarget();
        animator.SetTrigger("attackStrong");
    }

    public void ShootFireball()
    {
        var fireballObj = Instantiate(fireball,firingPlace.transform.position,Quaternion.identity);
        fireballObj.GetComponent<Rigidbody>().AddForce(transform.forward*20, ForceMode.Impulse);
    }

    public void PlaceFire()
    {
        var fireAreaObj = Instantiate(fireArea,Player.transform.position,Quaternion.identity);
        fireAreaObj.GetComponent<FireArea>().copied = true;
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
}
