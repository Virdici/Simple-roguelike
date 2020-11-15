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

    public Animator animator;
    PlayerMovement movement;

    void Start()
    {
        CurrentHP = HP;
        movement = GetComponent<PlayerMovement>();

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
        if (Input.GetMouseButtonDown(0))
        {
            //movement.isAttacking = true;
            animator.SetBool("IsAttacking", true);
            animator.SetTrigger("IsAttackingg");
            //StartCoroutine(attack());



            // movement.isAttacking = false;


        }
    }
    IEnumerator attack()
    {

        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1f)
            yield return null;

        //movement.isAttacking = false;
        animator.SetBool("IsAttacking", false);

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
