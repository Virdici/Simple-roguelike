using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindDoors : MonoBehaviour
{
    public List<GameObject> list = new List<GameObject>();
    public Module ParentModule;

    void Start()
    {
        ParentModule = transform.GetComponentInParent<Module>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "door")
        {
            //Debug.Log("Doors Added");
            //list.Add(other.gameObject.transform.parent.gameObject);
            ParentModule.Doors.Add(other.gameObject.transform.parent.gameObject);
        }
    }
 
}
