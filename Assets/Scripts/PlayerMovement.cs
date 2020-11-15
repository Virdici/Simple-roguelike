// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;


// public class PlayerMovement : MonoBehaviour
// {
//     public CharacterController controller;
//     public float speed = 10f;
//     public float gravity = -9.81f;
//     public float jumpHeight = 5f;
//     public Transform groundCheck;
//     public float groundDistance = 0.4f;
//     public LayerMask groundMask;



//     Vector3 velocity;
//     bool isGrounded;
//     //float slopeForceRayLength = 10f;
//     bool onSlope;
//     bool ResetPosition = false;
//     public Animator animator;



//     public const float maxDashTime = 1.0f;
//     public float dashDistance = 3;
//     public float dashStoppingSpeed = 0.1f;
//     float currentDashTime = maxDashTime;
//     float dashSpeed = 2;

//     float walkSpeed = 0f;


//     private void Start()
//     {
//         animator = GetComponentInChildren<Animator>();
//     }
//     // private bool OnSlope()
//     // {
//     //     RaycastHit hit;

//     //     if (Physics.Raycast(transform.position, Vector3.down, out hit, controller.height / 2 * slopeForceRayLength))
//     //         if (hit.normal != Vector3.up)
//     //             return true;
//     //     return false;

//     //     onSlope = OnSlope();  // pod is grounded w update
//     //     if (OnSlope())
//     //     {
//     //         velocity.y = gravity * 30000f;

//     //         if (Input.GetKeyDown(KeyCode.Space) && !isGrounded)
//     //         {
//     //             velocity.y = 0f;
//     //             velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
//     //         }
//     //     }
//     // }
//     void Update()
//     {
//         isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);



//         if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
//         {
//             currentDashTime = 0f;
//             currentDashTime = 0;
//         }
//         if (currentDashTime < maxDashTime)
//         {

//             //tutaj wyłączyć hitboxy na kilka klatek chyba

//             velocity = transform.forward * dashDistance * dashSpeed;
//             currentDashTime += dashStoppingSpeed;
//         }
//         else
//         {
//             velocity = new Vector3(0, velocity.y, 0);
//         }



//         if (isGrounded && velocity.y < 0)
//         {
//             velocity.y = -2f;
//         }

//         float x = Input.GetAxis("Horizontal");
//         float z = Input.GetAxis("Vertical");

//         Vector3 move = transform.right * x + transform.forward * z;

//         controller.Move(move * speed * Time.deltaTime);

//         if (Input.GetKey(KeyCode.LeftShift))
//         {
//             controller.Move(move * speed * 1.3f * Time.deltaTime);
//             velocity.y += gravity * Time.deltaTime;
//         }
//         velocity.y += gravity * Time.deltaTime;

//         controller.Move(velocity * Time.deltaTime);

//         if (ResetPosition == true)
//         {
//             transform.position = new Vector3(0, 1, 0);
//             ResetPosition = false;
//         }
//         walkSpeed = z;
//         animator.SetFloat("Speed",walkSpeed);

//     }

//     public void ResetPositionz()
//     {
//         ResetPosition = true;
//     }

// }



using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    bool ResetPosition = false;
    public Animator animator;
    public float speed = 5f;
    public float smoothTime = 0.1f;
    float currentVelocity;
    public Transform cam;
    bool isGrounded;
    public float groundDistance = 0.4f;
    public Transform groundCheck;
    public LayerMask groundMask;
    Vector3 velocity;
    public float gravity = -9.81f;
    public const float maxDashTime = 1.0f;
    public float dashDistance = 3;
    public float dashStoppingSpeed = 0.1f;
    float currentDashTime = maxDashTime;
    float dashSpeed = 2;
    public float horizontal;
    public float vertical;
    public bool isAttacking = false;
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        Cursor.lockState = CursorLockMode.Locked;

    }

    void Update()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isAttacking)
        {
            horizontal = 0;
            vertical = 0;
        }
        else
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
        }
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, smoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            controller.Move(moveDirection.normalized * speed * Time.deltaTime);

            if (Input.GetKey(KeyCode.LeftShift))
            {
                controller.Move(moveDirection.normalized * speed * 1.3f * Time.deltaTime);
                vertical *=2;
                horizontal*=2;
            }

        }
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            currentDashTime = 0f;
            currentDashTime = 0;
        }
        if (currentDashTime < maxDashTime)
        {

            //tutaj wyłączyć hitboxy na kilka klatek chyba

            velocity = transform.forward * dashDistance * dashSpeed;
            currentDashTime += dashStoppingSpeed;
        }
        else
        {
            velocity = new Vector3(0, velocity.y, 0);
        }


        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        animator.SetFloat("y", vertical);
        animator.SetFloat("x", horizontal);


    }

    public void ResetPositionz()
    {
        ResetPosition = true;
    }

}
