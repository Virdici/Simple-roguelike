using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    private void OnTriggerStay(Collider other) 
    {
        if(other.gameObject.transform.root.name == "player")
        {
            other.gameObject.transform.position =  transform.position ;
        }
    }
}
