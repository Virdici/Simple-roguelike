using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    bool ResetPosition = false;
    public Animator animator;
    public float speed = 5f;
    public float runMultiplier = 2;
    public float smoothTime = 0.1f;
    float currentVelocity;
    public Transform cam;
    public bool isGrounded;
    public float groundDistance = 0.4f;
    public Transform groundCheck;
    public LayerMask groundMask;
    Vector3 velocity;
    public float gravity = -9.81f;
    public const float maxDashTime = 2f;
    public float dashDistance = 10;
    public float dashStoppingSpeed = 0.1f;
    float currentDashTime = maxDashTime;
    float dashSpeed = 6;
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
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

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
                controller.Move(moveDirection.normalized * speed * runMultiplier * Time.deltaTime);
                vertical *= 2;
                horizontal *=  2;
                animator.SetTrigger("Run"); 
            }
        }
        if (velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentDashTime = 0f;
        }
        if (currentDashTime < maxDashTime)
        {
            animator.SetBool("isEvading", true);
            //tutaj wyłączyć hitboxy na kilka klatek chyba
            velocity = transform.forward * dashDistance;
            currentDashTime += dashStoppingSpeed;
        }
        else
        {
            animator.SetBool("isEvading", false);
            velocity = Vector3.zero;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime * dashSpeed);

        animator.SetFloat("y", vertical);
        animator.SetFloat("x", horizontal);
    }
    public void ResetPositionz()
    {
        ResetPosition = true;
    }
}
