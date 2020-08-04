using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindCubes : MonoBehaviour
{
    public List<GameObject> list = new List<GameObject>();
    public Collider[] hitColliders;
    //private void Start()
    //{
    //    hitColliders = Physics.OverlapSphere(new Vector3(100,100,100), 30f);
    //    foreach (var hitCollider in hitColliders)
    //    {
    //        hitCollider.SendMessage("AddDamage");
    //    }
    //}
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "door")
        list.Add(other.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
