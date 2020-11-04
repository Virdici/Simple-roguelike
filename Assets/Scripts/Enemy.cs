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


    int MaxDist = 5;
    int MinDist = 5;

    public float dist;
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        defeated = false;
    }



    // void Update()
    // {

    // if (Player.CurrentRoomIndex == index && defeated != true)
    // {
    //     var target = PlayerMarker.transform.position - transform.position;
    //     target.y = 0;
    //     var look = Quaternion.LookRotation(target);

    //     Vector3 correctLook = new Vector3(look.eulerAngles.x,0,look.eulerAngles.z);

    //     transform.rotation = Quaternion.Lerp(transform.rotation, look, 2f * Time.deltaTime);


    //     if (Vector3.Distance(transform.position, Player.transform.position) >= MinDist)
    //     {
    //         var targetPosition = new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z);
    //         transform.position = Vector3.MoveTowards(transform.position, targetPosition, 3f * Time.deltaTime);
    //         if (Vector3.Distance(transform.position, Player.transform.position) <= MaxDist)
    //         {
    //             transform.position = transform.position;
    //         }
    //     }
    // }   working chase without navmesh




    void Update()    //navmesh navigation working
    {

        if (Player.CurrentRoomIndex == index && defeated != true)
        {
            var target = PlayerMarker.transform.position - transform.position;
            var look = Quaternion.LookRotation(target);
            transform.rotation = Quaternion.Lerp(transform.rotation, look, 3f * Time.deltaTime);
            dist = Vector3.Distance(transform.position, Player.transform.position);

            // if (Vector3.Distance(transform.position, Player.transform.position) >= MinDist)
            // {
            //     Agent.isStopped = false;
            Agent.SetDestination(PlayerMarker.transform.position);

            if (Vector3.Distance(transform.position, Player.transform.position) <= MaxDist)
            {
                Agent.isStopped = true;
            }
            else
            {
                Agent.isStopped = false;

            }

            // }

        }

        if (defeated)
        {
            GameObject.Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            defeated = true;
            //transform.GetComponentInChildren<GameObject>().GetComponent<Renderer>().material.color = Color.red;
            //Agent.SetDestination(transform.position);


        }
    }

    //public void DealDamage(int damage)
    //{
    //    if (this.CurrentHP >= 0)
    //    {
    //        this.HP -= damage;
    //    }
    //    else
    //    {
    //        defeated = true;
    //        //transform.GetComponentInChildren<GameObject>().GetComponent<Renderer>().material.color = Color.red;
    //        Agent.SetDestination(transform.position);
    //    }
    //}
}
