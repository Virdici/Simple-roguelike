using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public bool defeated = false;

    private void Awake()
    {
        Physics.IgnoreLayerCollision(9, 11);
    }
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
        Debug.Log("Touched enemy");

    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Touched enemy");
        Physics.IgnoreLayerCollision(9, 11);
        defeated = true;
        transform.GetComponent<Renderer>().material.color = Color.red;
    }
}
