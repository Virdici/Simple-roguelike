using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Weapon : MonoBehaviour
{
    public GameObject wielder;


    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.transform.root.name);
        other.transform.root.Find("Cube").GetComponent<Renderer>().material.color = Color.red;
    }
}
