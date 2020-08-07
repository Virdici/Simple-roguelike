using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DoorCloser : MonoBehaviour
{
    private Door Door;
    public Module Room;
    public GameObject Seal;
    public bool RoomClear = false;

    private int RoomDoorsCount;
    private GameObject RoomSealsContainter;

    void Start()
    {
        RoomSealsContainter = GameObject.Find("RoomSeals");
        Physics.IgnoreLayerCollision(9, 10);
        Door = transform.GetComponentInParent<Door>();
        Room = (Module)GameObject.FindObjectsOfType<Module>().Where(m => m.index == Door.index && m.type == "room").FirstOrDefault();
        RoomDoorsCount = Room.Doors.Count();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (RoomClear == false)
        {
            foreach (var door in Room.Doors)
            {
                var doorPosition = new Vector3(door.transform.position.x, door.transform.position.y, door.transform.position.z);
                var sil = Instantiate(Seal, doorPosition, Quaternion.identity);
                sil.transform.SetParent(RoomSealsContainter.transform);
            }
        }
    }

    private void Update()
    {
        RoomClear = Room.AllenemiesDefeated;


    }
}
