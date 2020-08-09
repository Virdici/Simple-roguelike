using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public bool defeated = false;
    public Player Player;
    public int index;

    private bool flagged = false;
    int MoveSpeed = 4;
    int MaxDist = 10;
    int MinDist = 5;
    void Start()
    {
        defeated = false;
    }

    void Update()
    {

        if (Player.CurrentRoomIndex == index && flagged != true)
        {
            transform.GetComponent<Renderer>().material.color = Color.green;
            flagged = true;
        }

        if (Player.CurrentRoomIndex == index && defeated != true)
        {
            var target = Player.transform.position;
            target.y = 0f;
            transform.LookAt(target);

            if (Vector3.Distance(transform.position, Player.transform.position) >= MinDist)
            {
                var targetPosition = new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z);
                //transform.position += targetPosition * MoveSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, MoveSpeed * Time.deltaTime);



                if (Vector3.Distance(transform.position, Player.transform.position) <= MaxDist)
                {
                    //Here Call any function U want Like Shoot at here or something
                }
            }
        }
    }
    private void OnCollisionStay(Collision collision)
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
        defeated = true;
        transform.GetComponent<Renderer>().material.color = Color.red;

        }
    }
}
