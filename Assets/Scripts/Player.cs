using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int CurrentRoomIndex;
    public Animator animator;
    PlayerMovement movement;

    void Start()
    {
        movement = GetComponent<PlayerMovement>();
    }

    public void resetPosition()
    {
        transform.position = new Vector3(0, 1, 0);
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool("IsAttacking", true);
            animator.SetTrigger("IsAttackingg");
        }
    }
}
