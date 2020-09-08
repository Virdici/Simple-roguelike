using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ladder : MonoBehaviour
{
    public GameObject PlayerObject;
    public bool CanClimb = false;
    public float ClimbSpeed = 2;
    private void Start()
    {
        Physics.IgnoreLayerCollision(14, 15);
    }
    private void OnCollisionEnter(Collision collision)
    {
        PlayerObject = collision.gameObject.transform.root.gameObject;
        

            CanClimb = true;
            PlayerObject.GetComponent<PlayerMovement>().gravity = 0;
        
    }

    private void OnCollisionExit(Collision collision)
    {
        
            CanClimb = false;
            PlayerObject.GetComponent<PlayerMovement>().gravity = -29.43f;
            PlayerObject = null;
        
    }

    private void Update()
    {
        if (CanClimb)
        {
            if (Input.GetKey(KeyCode.W))
            {
                PlayerObject.transform.Translate(new Vector3(0, 15, 0) * Time.deltaTime * ClimbSpeed);
            }
            if (Input.GetKey(KeyCode.S))
            {
                PlayerObject.transform.Translate(new Vector3(0, -15, 0) * Time.deltaTime * ClimbSpeed);
            }
        }
    }
}
