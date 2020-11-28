using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveDamage : MonoBehaviour
{
    public int damage = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 18 && GetComponentInParent<Combat>().active)
        {
            Debug.Log("hit " + other.gameObject.transform.root.name);
            other.GetComponentInParent<Health>().TakeDamage(damage);
        }
    }
}
