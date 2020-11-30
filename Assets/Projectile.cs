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
    }

    private void OnTriggerEnter(Collider other)
    {
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
