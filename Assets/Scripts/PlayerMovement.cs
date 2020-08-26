using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 10f;
    public float gravity = -9.81f;
    public float jumpHeight = 5f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;


    Vector3 velocity;
    bool isGrounded;
    float slopeForceRayLength = 10f;
    bool onSlope;
    bool ResetPosition = false;
    private bool OnSlope()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, controller.height / 2 * slopeForceRayLength))
            if (hit.normal != Vector3.up)
            return true;
        return false;
    }
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        onSlope = OnSlope();
        if (OnSlope())
        {
            //velocity.y = gravity  *  10f; 

            if (Input.GetKeyDown(KeyCode.Space) && !isGrounded)
            {
                velocity.y = 0f;
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }



        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x  + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);


        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }


        if (Input.GetKey(KeyCode.LeftShift))
        {
            controller.Move(move * speed * 2f * Time.deltaTime);
        }
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (ResetPosition == true)
        {
            transform.position = new Vector3(0, 1, 0);
            ResetPosition = false;
        }

    }

    public void ResetPositionz()
    {
        ResetPosition = true;
    }

}
