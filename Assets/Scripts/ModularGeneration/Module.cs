﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Module : MonoBehaviour
{
    public string type;
    public int index;
    public List<GameObject> Doors = new List<GameObject>();
    public List<Enemy> Enemies = new List<Enemy>();
    public bool AllenemiesDefeated;
    public int DefeatedEnemies;

    private void Start()
    {
        AllenemiesDefeated = false;
    }

    public Connector[] GetConnectors()
    {
        return GetComponentsInChildren<Connector>();
    }
    public string GetTypeName()
    {
        return type;
    }
    public void SetIndex(int i)
    {
        index = i;
    }

    private void Update()
    {
       DefeatedEnemies = Enemies.Where(e => e.defeated == true).Count();
        if (Enemies.Count == DefeatedEnemies) AllenemiesDefeated = true;
    }
}
