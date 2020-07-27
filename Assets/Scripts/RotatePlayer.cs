using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class RotatePlayer : MonoBehaviour
{

    public Transform player;
    public CinemachineFreeLook camera;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        player.Rotate(Vector3.up * camera.m_XAxis.Value);
        camera.m_XAxis.Value = 0f;
    }


}
