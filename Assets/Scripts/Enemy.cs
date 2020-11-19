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
    private bool attacking = false;
    private float timer;
    private float timerLimit;
    private int randomAction;
    [SerializeField]
    private bool canChase = true;
    private bool didHeal = false;
    private EnemyState state;
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        defeated = false;
        animator.applyRootMotion = false;
    }
    void Update()    //navmesh navigation working
    {
        currentDistance = Vector3.Distance(transform.position, Player.transform.position);
        // switch (state)
        // {
        //     case EnemyState.idle:
        //         DoIdle();
        //         if (Player.CurrentRoomIndex == index && defeated != true && attacking != true)
        //         {
        //             state = EnemyState.chase;
        //         }
        //         break;
        //     case EnemyState.chase:
        //         DoChase();
        //         if (currentDistance >= 2 && currentDistance <= 4)
        //         {
        //             state = EnemyState.lightAttack;
        //         }
        //         break;
        //     case EnemyState.lightAttack:
        //         DoLightAttack();
        //         state = EnemyState.chase;
        //         break;
        //     case EnemyState.strongAttack:
        //         DoChase();
        //         break;
        //     case EnemyState.retreat:
        //         DoChase();
        //         break;
        //     case EnemyState.heal:
        //         DoChase();
        //         break;
        // }

        DoChase();
        // if (Player.CurrentRoomIndex == index && defeated != true && attacking != true)
        // {
        //     currentDistance = Vector3.Distance(transform.position, Player.transform.position);
        //     // if (currentDistance > minDistanceFromPlayer && canChase == true)
        //     // {
        //     //     DoChase();
        //     // }

        //     // if (currentDistance < minDistanceFromPlayer)
        //     // {
        //     //     DoLightAttack();
        //     // }

        //     // if (GetComponent<Health>().CurrentHP <= 30)
        //     // {
        //     //     canChase = false;
        //     //     DoRetreat();
        //     // }

        //     // if (currentDistance >= 10 && didHeal == false)
        //     // {
        //     //     DoHeal();
        //     //     if(GetComponent<Health>().CurrentHP > 30) canChase = true;
        //     // }


        // }

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
        Agent.isStopped = true;
        animator.SetFloat("y", 0);
        animator.SetFloat("x", 0);

    }
    void DoChase()
    {
        // LookAtTarget();

        animator.SetFloat("y", 1, 1f, Time.deltaTime * 10f);
        var target = PlayerMarker.transform.position - transform.position;
        var look = Quaternion.LookRotation(target);
        transform.rotation = Quaternion.Lerp(transform.rotation, look, 3f * Time.deltaTime);
        currentDistance = Vector3.Distance(transform.position, Player.transform.position);


        Agent.SetDestination(PlayerMarker.transform.position);
    }

    void DoLightAttack()
    {
        StartCoroutine(LightAttackRoutine());
    }

    IEnumerator LightAttackRoutine()
    {
        LookAtTarget();
        animator.SetFloat("y", 0, 1f, Time.deltaTime * 10f);
        animator.SetTrigger("attackLight");

        yield return null;

    }

    void DoStrongAttack()
    {
        LookAtTarget();
        animator.SetFloat("y", 0, 1f, Time.deltaTime * 10f);
        animator.SetTrigger("attackStrong");
    }

    void DoRetreat(float stateTime)
    {
        // animator.SetFloat("y", -1, 1f, Time.deltaTime * 10f);
        StartCoroutine(RetreatRoutine(2f));
    }

    IEnumerator RetreatRoutine(float stateTime)
    {
        animator.SetFloat("y", -1, 1f, Time.deltaTime * 10f);
        yield return new WaitForSeconds(stateTime);
    }

    void DoHeal()
    {
        animator.SetFloat("y", 0, 1f, Time.deltaTime * 10f);
        animator.SetTrigger("heal");
        Debug.Log("healing");
        didHeal = true;
    }

    void LookAtTarget()
    {
        var target = PlayerMarker.transform.position - transform.position;
        var look = Quaternion.LookRotation(target);
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
        Agent.velocity = animator.deltaPosition / Time.deltaTime;
    }
}



// void Update()    //navmesh navigation working with some random actions NOT GOOD
//     {
//         if (Player.CurrentRoomIndex == index && defeated != true && attacking != true)
//         {
//             var target = PlayerMarker.transform.position - transform.position;
//             var look = Quaternion.LookRotation(target);
//             transform.rotation = Quaternion.Lerp(transform.rotation, look, 3f * Time.deltaTime);
//             currentDistance = Vector3.Distance(transform.position, Player.transform.position);

//             Agent.SetDestination(PlayerMarker.transform.position);

//             if (Vector3.Distance(transform.position, Player.transform.position) <= minDistanceFromPlayer)
//             {
//                 Agent.isStopped = true;
//                 // animator.SetBool("isWalking", false);
//                 animator.SetFloat("y", 0, 1f, Time.deltaTime * 10f);
//                 animator.SetTrigger("attackLight");

//             }
//             else
//             {
//                 Agent.isStopped = false;
//                 // animator.SetBool("isWalking", true);
//                 if (timer <= timerLimit)
//                 {
//                     timer += 1 * Time.deltaTime;
//                     switch (randomAction)
//                     {
//                         case var expression when (randomAction < 10 ):
//                             animator.SetFloat("y", 1, 1f, Time.deltaTime * 10f);
//                             // Debug.Log("forward");
//                             break;
//                         case var expression when (randomAction > 10 && randomAction < 12):
//                             animator.SetFloat("x", 1, 1f, Time.deltaTime * 10f);
//                             // Debug.Log("right");

//                             break;
//                         case var expression when (randomAction > 12 && randomAction < 14):
//                             animator.SetFloat("y", -1, 1f, Time.deltaTime * 10f);
//                             // Debug.Log("back");

//                             break;
//                         case var expression when (randomAction > 14 && randomAction < 16):
//                             animator.SetFloat("x", -1, 1f, Time.deltaTime * 10f);
//                             // Debug.Log("left");

//                             break;
//                     }
//                 }
//                 else
//                 {
//                     randomAction = Random.Range(0,16);
//                     if(randomAction>10)
//                     {
//                         timerLimit = Random.Range(4,8);
//                     }
//                     else
//                     {
//                         timerLimit = Random.Range(2,4);
//                     }
//                     timer = 0;

//                 }
//             }
//         }

//         if (defeated)
//         {
//             GameObject.Destroy(gameObject);
//         }

//         if (Input.GetKeyDown(KeyCode.C)) //enemy dash
//         {
//             currentDashTime = 0f;
//             currentDashTime = 0;
//             Agent.enabled = false;
//             attacking = true;

//         }

//         if (currentDashTime < maxDashTime * 2)
//         {
//             Rigidbody.AddForce(transform.forward / 2, ForceMode.Impulse);
//             currentDashTime += dashStoppingSpeed;
//         }
//         else
//         {
//             Agent.enabled = true;
//             Rigidbody.velocity = Vector3.zero;
//             attacking = false;
//         }

//     }






























// using System.Collections;
// using System.Collections.Generic;
// using System.Linq;
// using UnityEngine;
// using UnityEngine.AI;

// public class Enemy : MonoBehaviour
// {
//     public bool defeated = false;
//     public Player Player;
//     public GameObject PlayerMarker;
//     public int index;
//     public Rigidbody Rigidbody;
//     public NavMeshAgent Agent;
//     public Animator animator;
//     int minDistanceFromPlayer = 5;
//     public float currentDistance;
//     private bool attacking = false;
//     private float timer;
//     private float timerLimit;
//     private int randomAction;
//     void Start()
//     {
//         Agent = GetComponent<NavMeshAgent>();
//         defeated = false;
//     }
//     void Update()    //navmesh navigation working
//     {
//         if (Player.CurrentRoomIndex == index && defeated != true && attacking != true)
//         {
//             var target = PlayerMarker.transform.position - transform.position;
//             var look = Quaternion.LookRotation(target);
//             transform.rotation = Quaternion.Lerp(transform.rotation, look, 3f * Time.deltaTime);
//             currentDistance = Vector3.Distance(transform.position, Player.transform.position);

//             Agent.SetDestination(PlayerMarker.transform.position);

//             if (Vector3.Distance(transform.position, Player.transform.position) <= minDistanceFromPlayer)
//             {
//                 Agent.isStopped = true;
//                 // animator.SetBool("isWalking", false);
//                 animator.SetFloat("y", 0, 1f, Time.deltaTime * 10f);
//                 animator.SetTrigger("attackLight");

//             }
//             else
//             {
//                 Agent.isStopped = false;
//                 animator.SetFloat("y", 1, 1f, Time.deltaTime * 10f);

//                 // animator.SetBool("isWalking", true);

//             }
//         }

//         if (defeated)
//         {
//             GameObject.Destroy(gameObject);
//         }

//     }





//     private void OnCollisionEnter(Collision collision)
//     {
//         if (collision.transform.tag == "Player")
//         {
//             defeated = true;
//             //transform.GetComponentInChildren<GameObject>().GetComponent<Renderer>().material.color = Color.red;
//             //Agent.SetDestination(transform.position);
//         }
//     }

//     private void OnAnimatorMove()
//     {
//         Agent.velocity = animator.deltaPosition / Time.deltaTime;
//     }
// }
