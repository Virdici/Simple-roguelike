using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{

    public Health health;
    public Animator animator;
    public bool active;

    public void activateHitBoxEvent()
    {
        active = true;
        // Debug.Log("activated hitbox");
    }

    public void deactivateHitBoxEvent()
    {
        active = false;
        // Debug.Log("Deactivated hitbox");
    }
}
