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

 
    int MaxDist = 10;
    int MinDist = 5;
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        defeated = false;
    }



    void Update()
    {

        if (Player.CurrentRoomIndex == index && defeated != true)
        {
            var target = PlayerMarker.transform.position - transform.position;
            var look = Quaternion.LookRotation(target);
            transform.rotation = Quaternion.Lerp(transform.rotation, look, 2f * Time.deltaTime);
           

            if (Vector3.Distance(transform.position, Player.transform.position) >= MinDist)
            {
                var targetPosition = new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, 1f * Time.deltaTime);

                if (Vector3.Distance(transform.position, Player.transform.position) <= MaxDist)
                {
                    transform.position = transform.position;
                }
            }
        }

        if (defeated)
        {
            GameObject.Destroy(gameObject);
        }
    }

    //void Update()    navmesh navigation working
    //{


    //    if (Player.CurrentRoomIndex == index && defeated != true)
    //    {
    //        var target = PlayerMarker.transform.position - transform.position;
    //        var look = Quaternion.LookRotation(target);
    //        transform.rotation = Quaternion.Lerp(transform.rotation, look, 2f * Time.deltaTime);
    //        //transform.LookAt(PlayerMarker.transform.position);


    //        if (Vector3.Distance(transform.position, Player.transform.position) >= MinDist)
    //        {

    //            Agent.SetDestination(PlayerMarker.transform.position);

    //            if (Vector3.Distance(transform.position, Player.transform.position) <= MaxDist)
    //            {
    //                Agent.SetDestination(transform.position);
    //            }
    //        }
    //    }

    //    if (defeated)
    //    {
    //        GameObject.Destroy(gameObject);
    //    }
    //}
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
