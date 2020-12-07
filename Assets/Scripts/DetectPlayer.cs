using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{

    public int roomIndex;
    private void Update() {
        roomIndex = GetComponentInParent<Door>().index;
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            var player = (Player)GameObject.FindObjectOfType<Player>();
            player.CurrentRoomIndex = roomIndex;
        }
    }
}
