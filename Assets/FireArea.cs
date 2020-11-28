using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireArea : MonoBehaviour
{
    public bool canDealDamage;
    public Player player;
    public float damage = .2f;
    public bool copied = false;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 18 || other.gameObject.layer == 12)
        {
            GiveDamage();
        }
    }
    void GiveDamage()
    {
        player.GetComponentInParent<Health>().CurrentHP -= damage;

    }
    void DestroyFire()
    {
        if(copied)
            Object.Destroy(gameObject);

    }
}
