using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OutsideRoomTrigger : MonoBehaviour
{
    private Door Door;
    public Module Room;
    public Animator animator;
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
            Door.open = true;
            animator.SetBool("opened", true);
        }
    }
    private void Update()
    {
        RoomClear = Room.AllenemiesDefeated;
    }
}
