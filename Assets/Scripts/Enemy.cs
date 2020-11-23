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
    int minDistanceFromPlayer = 2;
    public float currentDistance;
    public bool isAttacking = false;
    private float timer;
    private float timerLimit;
    private int randomAction;
    private bool canChase = true;
    private bool didHeal = false;
    private EnemyState state;
    public bool isAnimationMovement = false;
    public bool goodHealth = true;
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        defeated = false;
        animator.applyRootMotion = true;
    }
    void Update()    //navmesh navigation working
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
                    if (Random.Range(0, 10) <= 6)
                    {
                        state = EnemyState.lightAttack;
                    }
                    else
                    {
                        state = EnemyState.strongAttack;
                    }
                }
                if (!goodHealth)
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
                if (!goodHealth)
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
                if (!goodHealth)
                {
                    state = EnemyState.retreat;
                }

                break;
            case EnemyState.retreat:
                DoRetreat();
                if (currentDistance >= 12)
                {
                    state = EnemyState.heal;
                }
                break;
            case EnemyState.heal:
                DoHeal();
                state = EnemyState.idle;
                break;
        }

        if (GetComponent<Health>().CurrentHP <= 40)
        {
            goodHealth = false;
        }
        else
        {
            goodHealth = true;
        }

        if (defeated)
        {
            GameObject.Destroy(gameObject);
        }
    }

    public enum EnemyState
    {
        idle,
        chase,
        lightAttack,
        strongAttack,
        retreat,
        heal
    }
    void DoIdle()
    {
        animator.SetBool("isWalking", false);
        animator.SetBool("isHealing", false);
    }
    void DoChase()
    {
        LookAtTarget();
        Agent.SetDestination(PlayerMarker.transform.position);
        animator.SetBool("isWalking", true);
        Debug.Log("chase");
    }
    void DoLightAttack()
    {
        LookAtTarget();
        animator.SetBool("isWalking", false);
        animator.SetTrigger("attackLight");
        Debug.Log("punching");
    }
    void DoStrongAttack()
    {
        LookAtTarget();
        isAttacking = true;
        animator.SetBool("isWalking", false);
        animator.SetTrigger("attackStrong");
    }
    void DoRetreat()
    {
        LookAway();
        animator.SetBool("isWalking", false);
        animator.SetBool("isRetreating", true);

        Debug.Log("retreat");
    }
    void DoHeal()
    {
        animator.SetBool("isRetreating", false);
        if (didHeal == false)
        {
            animator.SetBool("isHealing", true);
        }

        Debug.Log("healing");
        didHeal = true;
    }
    void LookAtTarget()
    {
        var target = PlayerMarker.transform.position - transform.position;
        var look = Quaternion.LookRotation(target);
        transform.rotation = Quaternion.Lerp(transform.rotation, look, 3f * Time.deltaTime);
    }
    void LookAway()
    {
        var target = PlayerMarker.transform.position - transform.position;
        var look = Quaternion.LookRotation(-target);
        transform.rotation = Quaternion.Lerp(transform.rotation, look, 3f * Time.deltaTime);
    }
    public void HealEvent()
    {
        GetComponent<Health>().CurrentHP += 60;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            defeated = true;
        }
    }
    private void OnAnimatorMove()
    {
        if (isAnimationMovement)
            Agent.velocity = animator.deltaPosition / Time.deltaTime;
    }
    void StartAnimationMovementEvent()
    {
        isAnimationMovement = true;
    }
    void StopAnimationMovementEvent()
    {
        isAnimationMovement = false;
    }
    void StopWalkingEvent()
    {
        Agent.isStopped = true;
        animator.SetBool("isWalking", false);

    }
    void ContinueWalkingEvent()
    {
        Agent.isStopped = false;
        animator.SetBool("isWalking", true);
    }
    void StartAttack()
    {
        isAttacking = true;
    }
    void StopAttack()
    {
        isAttacking = false;
        animator.SetBool("isLightAttack", false);
    }
}