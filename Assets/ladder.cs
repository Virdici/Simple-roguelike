// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using System.Linq;


// public class ladder : MonoBehaviour
// {
//     public GameObject PlayerObject;
//     public bool CanClimb = false;
//     public float ClimbSpeed = 0.5f;
//     public GameObject foots;

//     private void Start()
//     {
//         foots = PlayerObject.transform.Find("GroundCheck").gameObject;
//         Physics.IgnoreLayerCollision(14, 15);
//     }
//     private void OnCollisionEnter(Collision collision)
//     {
//         PlayerObject = collision.gameObject.transform.root.gameObject;


//             CanClimb = true;
//             PlayerObject.GetComponent<PlayerMovement>().gravity = 0;
//     }
//     private void OnCollisionExit(Collision collision)
//     {
//             CanClimb = false;
//             PlayerObject.GetComponent<PlayerMovement>().gravity = -29.43f;
//             PlayerObject = null;
//     }

//     private void Update()
//     {
//         if (CanClimb)
//         {
//             if (Input.GetKey(KeyCode.W))
//             {
//                 PlayerObject.transform.position = new Vector3(foots.transform.position.x,foots.transform.position.y+1f,foots.transform.position.z);
//                 //PlayerObject.transform.Translate(new Vector3(0, 10, 0) * Time.deltaTime );
//             }
//             if (Input.GetKey(KeyCode.S))
//             {
//                 //PlayerObject.transform.Translate(new Vector3(0, -10, 0) * Time.deltaTime );
//             }
//         }
//     }
// }




using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class ladder : MonoBehaviour
{
    public GameObject PlayerObject;
    public bool CanClimb = false;
    public bool isClimbing = false;
    public float ClimbSpeed = 0.5f;
    public GameObject foots;

    private void Start()
    {
        foots = PlayerObject.transform.Find("GroundCheck").gameObject;
        Physics.IgnoreLayerCollision(14, 15);
    }
private void OnCollisionEnter(Collision collision)
    {
        PlayerObject = collision.gameObject.transform.root.gameObject;


            CanClimb = true;
            PlayerObject.GetComponent<PlayerMovement>().gravity = 0;
    }
    private void OnCollisionExit(Collision collision)
    {
            CanClimb = false;
            PlayerObject.GetComponent<PlayerMovement>().gravity = -29.43f;
            PlayerObject = null;
    }
    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.name == "PlayerObject") CanClimb = true;
    // }
    // private void OnTriggerExit(Collider other)
    // {
    //     if (other.name == "PlayerObject") CanClimb = false;
    // }

    private void Update()
    {
        if (CanClimb)
        {
            

            
                if (Input.GetKey(KeyCode.W))
                {
                    //PlayerObject.transform.position = new Vector3(foots.transform.position.x,foots.transform.position.y+1f,foots.transform.position.z);
                    PlayerObject.transform.position += Vector3.up / 2;
                    //PlayerObject.transform.Translate(new Vector3(0, 10, 0) * Time.deltaTime );
                }
                if (Input.GetKey(KeyCode.S))
                {
                    //PlayerObject.transform.Translate(new Vector3(0, -10, 0) * Time.deltaTime );
                }

            
        }
    }
}
