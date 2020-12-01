using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 10;
    public Player Player;

    private void Start()
    {
        transform.LookAt(Player.transform);
        Physics.IgnoreLayerCollision(9,19);
        Physics.IgnoreLayerCollision(16,19);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        
        if (other.gameObject.layer == 12 && GetComponentInParent<Combat>().active && other.gameObject.tag == "Player")
        {
            other.GetComponentInParent<Health>().TakeDamage(damage);
        }
        if(other.gameObject.tag != "Enemy")
        {
            Object.Destroy(gameObject);
        }
    }
}
