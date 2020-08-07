using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public bool defeated = false;
    void Start()
    {
        Physics.IgnoreLayerCollision(9, 11);
        defeated = false;
    }

    void Update()
    {
        Physics.IgnoreLayerCollision(9, 11);
    }
    private void OnCollisionStay(Collision collision)
    {
        Physics.IgnoreLayerCollision(9, 11);

    }

    private void OnCollisionEnter(Collision collision)
    {
        Physics.IgnoreLayerCollision(9, 11);
        if (collision.transform.tag == "Player")
        {
        defeated = true;
        transform.GetComponent<Renderer>().material.color = Color.red;

        }
    }
}
