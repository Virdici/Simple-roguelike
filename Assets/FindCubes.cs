using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindCubes : MonoBehaviour
{
    public List<GameObject> list = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "door")
        list.Add(other.gameObject.transform.parent.gameObject);
    }

}
