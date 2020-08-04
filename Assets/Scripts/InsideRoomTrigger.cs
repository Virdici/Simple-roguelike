using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideRoomTrigger : MonoBehaviour
{
    private Module Door;

    void Start()
    {
        Door = transform.GetComponentInParent<Module>();
    }
    private void OnTriggerEnter(Collider other)
    {

        Physics.IgnoreLayerCollision(9, 10);

    }

    private void OnCollisionEnter(Collision collision)
    {
    }
    private void OnTriggerStay(Collider other)
    {
        Door = transform.GetComponentInParent<Module>();
        Physics.IgnoreLayerCollision(9, 10);
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log(other);
            Door.GetComponent<Renderer>().material.color = Color.red;
        }
    }

}
