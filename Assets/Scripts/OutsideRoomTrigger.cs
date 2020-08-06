using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideRoomTrigger : MonoBehaviour
{

    private Door Door;

    void Start()
    {
        Physics.IgnoreLayerCollision(9, 10);

        Door = transform.GetComponentInParent<Door>();
    }
    private void OnTriggerStay(Collider other)
    {
        //Physics.IgnoreLayerCollision(9, 10);
        if (Input.GetKeyDown(KeyCode.F))
        {
            Door.transform.Find("ColliderPassage").gameObject.SetActive(false);
            Door.GetComponent<Renderer>().material.color = Color.blue;
        }
    }

}
