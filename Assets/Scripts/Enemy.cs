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
    public int HP = 100;
    public int CurrentHP;

    public Rigidbody Rigidbody;
    public NavMeshAgent Agent;

    private bool flagged = false;
    int MoveSpeed = 4;
    int MaxDist = 4;
    int MinDist = 2;
    void Start()
    {
        CurrentHP = HP;
        Agent = GetComponent<NavMeshAgent>();
        defeated = false;
    }



    void Update()
    {
        

        if (Player.CurrentRoomIndex == index && defeated != true)
        {
            transform.LookAt(PlayerMarker.transform.position);
           

            if (Vector3.Distance(transform.position, Player.transform.position) >= MinDist)
            {

                Agent.SetDestination(PlayerMarker.transform.position);

                if (Vector3.Distance(transform.position, Player.transform.position) <= MaxDist)
                {
                    Agent.SetDestination(transform.position);
                }
            }
        }
    }
   private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            defeated = true;
            //transform.GetComponentInChildren<GameObject>().GetComponent<Renderer>().material.color = Color.red;
            Agent.SetDestination(transform.position);


        }
    }

    public void DealDamage(int damage)
    {
        if (this.CurrentHP >= 0)
        {
        this.HP -= damage;
        }
        else
        {
            defeated = true;
            //transform.GetComponentInChildren<GameObject>().GetComponent<Renderer>().material.color = Color.red;
            Agent.SetDestination(transform.position);
        }
    }
}
