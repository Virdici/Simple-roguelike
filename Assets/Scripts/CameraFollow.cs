using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float CameraMovementSpeed = 120f;
    public GameObject CameraFollowObject;
    Vector3 FollowPosition;
    public float ClampAngle = 80f;
    public float InputSensitivity = 150f;
    public GameObject CameraObject;
    public GameObject PlayerObject;
    public float CamDistanceXToPLayer;
    public float CamDistanceYToPLayer;
    public float CamDistanceZToPLayer;
    public float MouseX;
    public float MouseY;
    public float FinalInputX;
    public float FinalInputZ;
    public float smoothX;
    public float smoothY;
    private float rotationY = 0f;
    private float rotationX = 0f;


    void Start()
    {
        Vector3 rotation = transform.localRotation.eulerAngles;
        rotationY = rotation.y;
        rotationX = rotation.x;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float inputX = Input.GetAxis("RightStickHorizontal");
        float inputZ = Input.GetAxis("RightStickVertical");
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        FinalInputX =  mouseX;
        FinalInputZ =  mouseY;

        rotationY += FinalInputX * InputSensitivity * Time.deltaTime;
        rotationX += FinalInputZ * InputSensitivity * Time.deltaTime;
        rotationX = Mathf.Clamp(rotationX, -ClampAngle, ClampAngle);

        Quaternion localRotation = Quaternion.Euler(rotationX, rotationY, 0f);
        transform.rotation = localRotation;
    }

    private void LateUpdate()
    {
        CameraUpdater();
    }

    private void CameraUpdater()
    {
        Transform target = CameraFollowObject.transform;

        float step = CameraMovementSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);

    }
}
