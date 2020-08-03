using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideRoomTrigger : MonoBehaviour
{

    private Module Door;

    void Start()
    {
        Door = transform.GetComponentInParent<Module>();
    }
    private void OnTriggerEnter(Collider other)
    {

        Physics.IgnoreLayerCollision(8, 10);
        
    }


    private void OnTriggerStay(Collider other)
    {
        Physics.IgnoreLayerCollision(8, 10);
        Debug.Log("fucking wut" + other.transform);

        if (Input.GetKeyDown(KeyCode.F))
        {
            Door.GetComponent<Renderer>().material.color = Color.blue;
        }
    }

}
