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
    protected int lightAttackPropability;

    public virtual void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        defeated = false;
        animator.applyRootMotion = true;
        Physics.IgnoreLayerCollision(12, 18);
    }
    public virtual void Update()
    {
    }
    public virtual void FSM()
    {
        Debug.Log(state);
    }
    public enum EnemyState
    {
        idle,
        chase,
        lightAttack,
        strongAttack,
        strongAttack2,
        retreat,
        special,
        backoff,
        reposition
    }
    protected virtual void DoIdle()
    {
        animator.SetBool("isWalking", false);
    }
    protected virtual void DoChase()
    {
        animator.SetBool("isWalking", true);
        animator.ResetTrigger("LightAttack");
        animator.ResetTrigger("StrongAttack");
        LookAtTarget();
        Agent.SetDestination(PlayerMarker.transform.position);
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
    protected virtual void DoStrongAttack2()
    {
        LookAtTarget();
        animator.SetBool("isWalking", false);
        animator.SetTrigger("attackStrong2");
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

    protected virtual void DoReposition()
    {
        animator.SetBool("isRepositioning", true);
        Vector3 repositionTo = transform.position + ((transform.position - Player.transform.position) * 1);
        Agent.SetDestination(repositionTo);
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