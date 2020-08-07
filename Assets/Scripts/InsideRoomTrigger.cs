using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InsideRoomTrigger : MonoBehaviour
{
    private Door Door;
    public Module Room;
    public bool RoomClear = false;

    void Start()
    {
        Physics.IgnoreLayerCollision(9, 10);
        Door = transform.GetComponentInParent<Door>();
        Room = (Module)GameObject.FindObjectsOfType<Module>().Where(m => m.index == Door.index && m.type == "room").FirstOrDefault();
    }
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.F) && RoomClear == true)
        {
            Door.transform.Find("ColliderPassage").gameObject.SetActive(false);
            Door.GetComponent<Renderer>().material.color = Color.red;
        }
    }

    private void Update()
    {
        RoomClear = Room.AllenemiesDefeated;
    }

}
