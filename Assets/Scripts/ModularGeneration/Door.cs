using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int index;
    public bool AllenemiesDefeated;
    public int DefeatedEnemies;

    public bool DoorClosed = true;
    Animator animator;
    public bool open = false;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public Connector[] GetConnectors()
    {
        return GetComponentsInChildren<Connector>();
    }
    public void SetIndex(int i)
    {
        index = i;
    }
}
