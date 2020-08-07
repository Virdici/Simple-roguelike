using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSeal : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Physics.IgnoreLayerCollision(8, 13);
        Physics.IgnoreLayerCollision(8, 9);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
