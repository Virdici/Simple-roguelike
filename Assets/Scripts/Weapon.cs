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

    //private bool hasCollided = false;
    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {

        
        //try
        //{
        //    hit = other.GetComponentInParent<Enemy>().gameObject;
        //}
        //catch (System.Exception){}
        
        //try
        //{
        //    hit = other.GetComponentInParent<Player>().gameObject;
        //}
        //catch (System.Exception) { }

        
       
        // health = other.GetComponent<Health>();

        // if (health.CurrentHP != 0)
        // {
        //     if (hasCollided == false)
        //     {
        //         hasCollided = true;
        //         Debug.Log("ouch");
        //         health.TakeDamage(2);
        //     }
        // }
    }

    // private void OnTriggerExit(Collider other)
    // {
    //     hasCollided = false;
    // }
}
