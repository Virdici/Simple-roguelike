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
    void Update()
    {
        
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
