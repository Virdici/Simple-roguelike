using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Module : MonoBehaviour
{
    public string type;
    public int index;
    public List<GameObject> Doors = new List<GameObject>();
    public List<Enemy> Enemies = new List<Enemy>();
    public bool AllenemiesDefeated = false;
    public int DefeatedEnemies;
    private GameObject RoomSealsContainter;

    public bool PlacedSeals = false;

    private void Awake()
    {
        AllenemiesDefeated = false;

        RoomSealsContainter = GameObject.Find("RoomSeals Container");

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
        DefeatedEnemies = Enemies.Where(e => e == null).Count();
        if (Enemies.Count == DefeatedEnemies)
        {
            AllenemiesDefeated = true;
        }
        else
        {
            AllenemiesDefeated = false;
        }
        var player = (Player)GameObject.FindObjectOfType<Player>();

        if (AllenemiesDefeated == true && player.CurrentRoomIndex == index)
        {
            foreach (Transform child in RoomSealsContainter.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }

    }

    
}
