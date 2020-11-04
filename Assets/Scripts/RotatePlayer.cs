using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class RotatePlayer : MonoBehaviour
{

    public Transform player;
    public new CinemachineFreeLook camera ;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
       // var XAxis = Mathf.Lerp(1f, camera.m_XAxis.Value,2f * Time.deltaTime);

        player.Rotate(Vector3.up * camera.m_XAxis.Value);
        //player.Rotate(Vector3.up * XAxis);
        //player.rotation = Quaternion.Lerp(player.rotation, Quaternion.Euler(Vector3.up * camera.m_XAxis.Value), 100f*Time.deltaTime);
        camera.m_XAxis.Value = 0f;
    }


}
