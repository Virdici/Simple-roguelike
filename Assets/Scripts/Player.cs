using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int CurrentRoomIndex;
    public GameObject CurrentRoom;
    public bool defeated = false;
    public int HP = 100;
    public int CurrentHP;
   
    void Start()
    {
        CurrentHP = HP;
    }

    public void resetPosition()
    {
            Debug.Log("???");

        transform.position = new Vector3(0, 1, 0);

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            transform.position = new Vector3(0, 1, 0);

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
        }
    }

}
