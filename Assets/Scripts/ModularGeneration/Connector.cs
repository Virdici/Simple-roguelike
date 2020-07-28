using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Connector : MonoBehaviour
{
    public string[] allowedTypes;
    public bool startingConnector;
    void OnDrawGizmos()
    {
        Vector3 cubeSize = new Vector3(0.3f, 0.3f, 0.3f);
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(transform.position, cubeSize);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.right * 1f);
        Gizmos.DrawLine(transform.position, transform.position - transform.right * 1f);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 3f);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + transform.up * 1f);
    }

}
