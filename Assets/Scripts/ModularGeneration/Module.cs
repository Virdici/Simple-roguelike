using System.Collections.Generic;
using UnityEngine;

public class Module : MonoBehaviour
{
    public string type;
    public int index;
    public List<GameObject> Doors = new List<GameObject>();
    public Connector[] GetConnectors()
    {
        return GetComponentsInChildren<Connector>();
    }

    public string GetTypeName()
    {
        return type;
    }

    public void SetIndex(int inde)
    {
        this.index = inde;
    }


}
