using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireArea : MonoBehaviour
{
    public bool canDealDamage = false;
    public Player player;
    public float damage = .2f;
    public bool copied = false;
    
    private void Start() {
        StartCoroutine(startAttackCountdown());
    }

    private IEnumerator startAttackCountdown()
    {
        canDealDamage = false;
        yield return new WaitForSeconds(1f);
        canDealDamage = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if ((other.gameObject.layer == 18 || other.gameObject.layer == 12) && canDealDamage == true)
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
