using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InsideRoomTrigger : MonoBehaviour
{
    private Door Door;
    public Module Room;
    public bool RoomClear = false;
    private GameObject RoomSealsContainter;
    public GameObject Seal;
    public Animator animator;

    void Awake()
    {
        Physics.IgnoreLayerCollision(9, 10);
        Door = transform.GetComponentInParent<Door>();
        RoomSealsContainter = GameObject.Find("RoomSeals Container");
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

    private void OnTriggerEnter(Collider other)
    {
        if (RoomClear == false && Room.PlacedSeals == false)
        {
            foreach (var door in Room.Doors)
            {
                var doorPosition = new Vector3(door.transform.position.x, door.transform.position.y, door.transform.position.z);
                Door.open = false;
                animator.SetBool("opened", false);
            }
            Room.PlacedSeals = true;
        }
    }

    private void Update()
    {
        RoomClear = Room.AllenemiesDefeated;
    }

}
