using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int index;
    public bool AllenemiesDefeated;
    public int DefeatedEnemies;

    public bool DoorClosed = true;

    public Connector[] GetConnectors()
    {
        return GetComponentsInChildren<Connector>();
    }
    public void SetIndex(int i)
    {
        index = i;
    }

    private void Update()
    {
        
    }
}
