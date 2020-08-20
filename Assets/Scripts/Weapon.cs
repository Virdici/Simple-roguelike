using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Weapon : MonoBehaviour
{
    public GameObject wielder;
    public int Damage = 10;

    private bool IsWeaponIgnoringActive;
    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "weapon")
        {
            Physics.IgnoreCollision(other.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
            IsWeaponIgnoringActive = true;
            
        }

        Debug.Log(other.transform.root.name);
        if (other.transform.root.GetComponent<Enemy>() && IsWeaponIgnoringActive == true)
        {
            other.transform.root.Find("Cube").GetComponent<Renderer>().material.color = Color.red;
            other.transform.root.GetComponent<Enemy>().DealDamage(Damage);
        }

        if (other.transform.root.GetComponent<Player>() && IsWeaponIgnoringActive == true)
        {
            other.transform.root.GetComponent<Player>().DealDamage(Damage);
        }


    }
}
