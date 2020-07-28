using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform target;
    public Transform playerBody;
    public float distanceFromTarget = 3;
    public Vector2 verticalMinMax = new Vector2(-40, 85);

    float horizontalView;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        horizontalView = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        //verticalView = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        //verticalView = Mathf.Clamp(verticalView, verticalMinMax.x, verticalMinMax.y);

        //xRotation -= verticalView;
        //xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        //transform.position = target.position - transform.forward * distanceFromTarget;
        playerBody.Rotate(Vector3.up * horizontalView);
    }

}
