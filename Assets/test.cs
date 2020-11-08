// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using System.Linq;
// using System.Reflection;
// using System.Security.Cryptography;
// using UnityEngine.AI;

// public class test : MonoBehaviour
// {
//     public GameObject PlayerObject;

//     public Module startingModule;
//     public GameObject DungeonContainter;
//     public GameObject playerObject;

//     public Module[] Rooms;
//     public Module[] passages;
//     public List<Connector> connectors;
//     public int roomCount = 10;

//     void Start()
//     {
//         StartCoroutine(Starte(DungeonContainter));
        
//     }
//     public IEnumerator Starte(GameObject dungeonContainter)
//     {
//         DungeonContainter = dungeonContainter;

//         var firstModule = (Module)Instantiate(startingModule, dungeonContainter.transform.position, Quaternion.identity);
//         firstModule.transform.SetParent(DungeonContainter.transform);
//         var availableConnectors = new List<Connector>(firstModule.GetConnectors());
//         playerObject.transform.position = new Vector3(firstModule.transform.position.x, firstModule.transform.position.y + 1, firstModule.transform.position.z);
//         playerObject.transform.rotation = Quaternion.identity;
//         var selectedAvailableConnector = availableConnectors.ElementAt(Random.Range(0, availableConnectors.Count));
//         int i = 1;
//         while (i <= roomCount)
//         {

//             var randomPassage = passages[Random.Range(0, passages.Length)];
//             var passage = (Module)Instantiate(randomPassage, new Vector3(20, 0 , 1), Quaternion.identity);
//             passage.transform.SetParent(DungeonContainter.transform);
//             var passageConnector = passage.GetConnectors().ElementAt(Random.Range(0, passage.GetConnectors().Length));

//             Connect(selectedAvailableConnector, passageConnector);

//             yield return new WaitForSeconds(0.0001f);
//             var passageExitConnector = passage.GetConnectors().ElementAt(Random.Range(0, passage.GetConnectors().Length));

//             var randomRoom = Rooms[Random.Range(0, Rooms.Length)];
//             var room = (Module)Instantiate(randomRoom, new Vector3(20, 0, 1), Quaternion.identity);
//             room.transform.SetParent(DungeonContainter.transform);
//             var roomConnector = room.GetConnectors().ElementAt(Random.Range(0, room.GetConnectors().Length));
//             if (i == 4)
//             {
//                 room.GetComponent<Renderer>().material.color = Color.blue;
//             }
//             if (i == 10)
//             {
//                 room.GetComponent<Renderer>().material.color = Color.red;
//             }
//             room.index = i;
//             Connect(passageExitConnector, roomConnector);

//             yield return new WaitForSeconds(0.0001f);

//             selectedAvailableConnector = DungeonContainter.GetComponentsInChildren<Connector>().ToList().ElementAt(Random.Range(0, DungeonContainter.GetComponentsInChildren<Connector>().Length));
//             i++;
//         }
//         connectors = DungeonContainter.GetComponentsInChildren<Connector>().ToList();
        

//     }
//     private void Connect(Connector startingObject, Connector ObjectToConnect)
//     {
//         var newModule = ObjectToConnect.transform.parent;
//         //var objectToConnectVector = -startingObject.transform.forward;

//         var angle1 = Vector3.Angle(Vector3.forward, -startingObject.transform.forward) * Mathf.Sign(-startingObject.transform.forward.x);
//         var angle2 = Vector3.Angle(Vector3.forward, ObjectToConnect.transform.forward) * Mathf.Sign(ObjectToConnect.transform.forward.x);
  
//         newModule.RotateAround(ObjectToConnect.transform.position, Vector3.up, angle1 - angle2);
//         var correctPosition = startingObject.transform.position - ObjectToConnect.transform.position;
//         newModule.transform.position += correctPosition;

//         if (ObjectToConnect)
//         {
//             Destroy(startingObject.gameObject);
//             Destroy(ObjectToConnect.gameObject);
//         }
//     }

//     private void Update()
//     {
//         if (Input.GetKeyDown(KeyCode.J))
//         {
        
//             var foots = PlayerObject.transform.Find("GroundCheck").gameObject;


//         }
//     }
// }




using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using UnityEngine.AI;

public class test : MonoBehaviour
{
    
//    Rigidbody rigidbody;
//    public bool open = false;
//     void Start()
//     {
        
//         rigidbody = GetComponent<Rigidbody>();
        
//     }
    
//     private void Update()
//     {
//         if (Input.GetKeyDown(KeyCode.J))
//         {
//            rigidbody.AddForce(transform.forward*30,ForceMode.Impulse);
//         }
//     }
}



