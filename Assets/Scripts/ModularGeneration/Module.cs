using UnityEngine;

public class Module : MonoBehaviour
{
    public string type;
    public int index;
    public Connector[] GetConnectors()
    {
        return GetComponentsInChildren<Connector>();
    }
}
