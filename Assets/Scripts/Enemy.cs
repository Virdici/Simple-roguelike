using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public bool defeated = false;
    public Player Player;
    public GameObject PlayerMarker;
    public int index;
    public Rigidbody Rigidbody;
    public NavMeshAgent Agent;
    public Animator animator;
    public float currentDistance;
    public bool isAttacking = false;
    public bool didSpecial = false;
    protected EnemyState state;
    public bool isAnimationMovement = false;
    public bool goodHealth = true;
    public float defenceMultiplier = 1f;
    protected int attackPropability;
    protected int lightAttackPropability = 6;
    public virtual void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        defeated = false;
        animator.applyRootMotion = true;
    }
    public virtual void Update()
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
        attackPropability = Random.Range(0, 10);
    }
    public virtual void FSM()
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
                    state = EnemyState.chase;
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
                    state = EnemyState.chase;
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
    public enum EnemyState
    {
        idle,
        chase,
        lightAttack,
        strongAttack,
        retreat,
        special,
        backoff
    }
    protected virtual void DoIdle()
    {
        animator.SetBool("isWalking", false);
        animator.SetBool("isHealing", false);
    }
    protected virtual void DoChase()
    {
        LookAtTarget();
        Agent.SetDestination(PlayerMarker.transform.position);
        animator.SetBool("isWalking", true);
    }
    protected virtual void DoLightAttack()
    {
        LookAtTarget();
        animator.SetBool("isWalking", false);
        animator.SetTrigger("attackLight");
    }
    protected virtual void DoStrongAttack()
    {
        LookAtTarget();
        animator.SetBool("isWalking", false);
        animator.SetTrigger("attackStrong");
    }
    protected virtual void DoRetreat()
    {
        LookAway();
        animator.SetBool("isWalking", false);
        animator.SetBool("isRetreating", true);
    }
    protected virtual void DoSpecial()
    {
        animator.SetBool("isRetreating", false);
        if (didSpecial == false)
        {
            animator.SetBool("isHealing", true);
        }
        didSpecial = true;
    }
    protected virtual void LookAtTarget()
    {
        var target = PlayerMarker.transform.position - transform.position;
        var look = Quaternion.LookRotation(target);
        transform.rotation = Quaternion.Lerp(transform.rotation, look, 3f * Time.deltaTime);
    }
    protected virtual void LookAway()
    {
        var target = PlayerMarker.transform.position - transform.position;
        var look = Quaternion.LookRotation(-target);
        transform.rotation = Quaternion.Lerp(transform.rotation, look, 3f * Time.deltaTime);
    }
    protected virtual void HealEvent()
    {
        GetComponent<Health>().CurrentHP += 60;
    }
    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            defeated = true;
        }
    }
    protected virtual void OnAnimatorMove()
    {
        if (isAnimationMovement)
            Agent.velocity = animator.deltaPosition / Time.deltaTime;
    }
    protected virtual void StartAnimationMovementEvent()
    {
        isAnimationMovement = true;
    }
    protected virtual void StopAnimationMovementEvent()
    {
        isAnimationMovement = false;
    }
    protected virtual void StopWalkingEvent()
    {
        Agent.isStopped = true;
        animator.SetBool("isWalking", false);
    }
    protected virtual void ContinueWalkingEvent()
    {
        Agent.isStopped = false;
        animator.SetBool("isWalking", true);
    }
    protected virtual void StartAttack()
    {
        isAttacking = true;
        Agent.isStopped = true;
        animator.SetBool("isWalking", false);
        isAnimationMovement = true;
    }
    protected virtual void StopAttack()
    {
        isAttacking = false;
        Agent.isStopped = false;
        animator.SetBool("isWalking", true);
        isAnimationMovement = false;
    }
}