using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Module : MonoBehaviour
{
    public string type;
    public Connector[] GetConnectors()
    {
        return GetComponentsInChildren<Connector>();
    }
}
