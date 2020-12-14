using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Connector : MonoBehaviour
{
    public bool startingConnector;
    void OnDrawGizmos()
    {
        Vector3 cubeSize = new Vector3(1f, 1f, 1f);
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(transform.position, cubeSize);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 3f);
    }

}
