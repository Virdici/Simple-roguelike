using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Weapon : MonoBehaviour
{
    public GameObject wielder;
    public int Damage = 10;
    public GameObject hit;
    public Health health;

    private bool hasCollided = false;
    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {

        //if (other.gameObject.tag == "weapon")
        //{
        //    Physics.IgnoreCollision(other.gameObject.GetComponent<Collider>(), GetComponent<Collider>());

        //}

        //if (other.transform.root.GetComponent<Enemy>() )
        //{
        //    other.transform.root.Find("Cube").GetComponent<Renderer>().material.color = Color.red;
        //    //other.transform.root.GetComponent<Enemy>().DealDamage(Damage);
        //}

        //if (other.transform.root.GetComponent<Player>() )
        //{
        //    other.transform.root.GetComponent<Player>().DealDamage(Damage);
        //}
        try
        {
            hit = other.GetComponentInParent<Enemy>().gameObject;
        }
        catch (System.Exception){}
        
        try
        {
            hit = other.GetComponentInParent<Player>().gameObject;
        }
        catch (System.Exception) { }
       
        health = hit.GetComponent<Health>();

        if (health.CurrentHP != 0)
        {
            if (hasCollided == false)
            {
                hasCollided = true;
                Debug.Log("ouch");
                health.TakeDamage(2);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        hasCollided = false;
    }
}
