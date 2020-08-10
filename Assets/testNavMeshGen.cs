using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class testNavMeshGen : MonoBehaviour
{
    public NavMeshSurface navMeshSurfaces;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Begin());
    }

    private IEnumerator Begin()
    {
        yield return new WaitForSeconds(0);
        navMeshSurfaces.BuildNavMesh();
    }

    // Update is called once per frame
    void Update()
    {
        

    }
}
