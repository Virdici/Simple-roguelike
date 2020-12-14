using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Module : MonoBehaviour
{
    public List<Enemy> Enemies = new List<Enemy>();
    public List<GameObject> Doors = new List<GameObject>();
    public string type;
    public int index;
    public bool AllenemiesDefeated = false;
    public int DefeatedEnemies;
    public bool PlacedSeals = false;
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
        DefeatedEnemies = Enemies.Where(e => e == null).Count();
        if (Enemies.Count == DefeatedEnemies)
        {
            AllenemiesDefeated = true;
        }
        else
        {
            AllenemiesDefeated = false;
        }
    }
}
