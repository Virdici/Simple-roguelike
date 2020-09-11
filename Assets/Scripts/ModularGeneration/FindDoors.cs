using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FindDoors : MonoBehaviour
{
    public Module ParentModule;

    void Start()
    {
        ParentModule = transform.GetComponentInParent<Module>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "door")
        {
            other.gameObject.transform.GetComponentInParent<Door>().index = ParentModule.index;
            other.gameObject.transform.GetComponentInParent<Door>().GetComponentsInChildren<InsideRoomTrigger>().FirstOrDefault().Room = ParentModule;
            ParentModule.Doors.Add(other.gameObject.transform.parent.gameObject);
        }

        if (other.gameObject.tag == "Player")
        {
            var player = (Player)GameObject.FindObjectOfType<Player>();
            player.CurrentRoomIndex = ParentModule.index;
        }

        if (other.gameObject.layer == 11)
        {
            var insideTrigger = other.bounds;
        }
    }
 
}
