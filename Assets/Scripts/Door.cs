using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int index;
    private void OnCollisionEnter(Collision col)
    {
        Debug.Log("collision detected" + col.contacts[0].point);
        //generator.RenewIfCollided();
    }
    public void SetDoorIndex(int inde)
    {
        this.index = inde;
    }
}
