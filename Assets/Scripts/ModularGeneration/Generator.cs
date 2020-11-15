using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using UnityEngine.AI;

public class Generator : MonoBehaviour
{
    public Module startingModule;
    public bool collided;
    public Module Seal;
    public Door Door;
    public GameObject DungeonContainter;
    public GameObject playerObject;

    public Module[] Rooms;
    public Module BossRoom;
    public Module[] passages;
    public List<Connector> connectors;
    public int roomCount = 10;

    public float waitTime = 0.5f;
    private void Update()
    {
        if (collided)
        {
            RenewIfCollided();
            
        }

    }

    public IEnumerator Starte(GameObject dungeonContainter)
    {

        //Random.InitState(999); //seedowanie do zapisu danego układu WAŻNEE!!!! poprawić przy kolizjach (np 999)


        DungeonContainter = dungeonContainter;

        var firstModule = (Module)Instantiate(startingModule, dungeonContainter.transform.position, Quaternion.identity);
        firstModule.transform.SetParent(DungeonContainter.transform);
        var availableConnectors = new List<Connector>(firstModule.GetConnectors());
        playerObject.transform.position = new Vector3(firstModule.transform.position.x, firstModule.transform.position.y + 1, firstModule.transform.position.z);
        playerObject.transform.rotation = Quaternion.identity;
        var selectedAvailableConnector = availableConnectors.ElementAt(Random.Range(0, availableConnectors.Count));
        int i = 1;
        while (i <= roomCount)
        {

            var randomPassage = passages[Random.Range(0, passages.Length)];
            var passage = (Module)Instantiate(randomPassage, new Vector3(2,  30, 1), Quaternion.identity);
            passage.transform.SetParent(DungeonContainter.transform);
            var passageConnector = passage.GetConnectors().ElementAt(Random.Range(0, passage.GetConnectors().Length));
            
            AddDoor(selectedAvailableConnector, true);
            Connect(selectedAvailableConnector, passageConnector);

            yield return new WaitForSeconds(waitTime);

            var passageExitConnector = passage.GetConnectors().ElementAt(Random.Range(0, passage.GetConnectors().Length));
                  

            Module randomRoom ;
            if (i == roomCount)
            {
                randomRoom = BossRoom;
                randomRoom.GetComponent<EnemySpawning>().isBossRoom = true;
            }
            else
            {
                randomRoom = Rooms[Random.Range(0, Rooms.Length)];
            }
            var room = (Module)Instantiate(randomRoom, new Vector3(2,  30, 1), Quaternion.identity);
            room.transform.SetParent(DungeonContainter.transform);
            var roomConnector = room.GetConnectors().ElementAt(Random.Range(0, room.GetConnectors().Length));

            

            if (i == 4)
            {
                room.GetComponent<Renderer>().material.color = Color.blue;
            }
            if (i == 10)
            {
                room.GetComponent<Renderer>().material.color = Color.red;
            }
            room.index = i;

            if(room.GetComponent<EnemySpawning>().maxEnemies == 0) 
            {
                room.GetComponent<EnemySpawning>().maxEnemies = 2;
            }
            
            
            AddDoor(passageExitConnector, false);
            Connect(passageExitConnector, roomConnector);

            yield return new WaitForSeconds(waitTime);

            selectedAvailableConnector = DungeonContainter.GetComponentsInChildren<Connector>().ToList().ElementAt(Random.Range(0, DungeonContainter.GetComponentsInChildren<Connector>().Length));
            i++;
        }
        connectors = DungeonContainter.GetComponentsInChildren<Connector>().ToList();
        StartCoroutine(SealEnds());

    }
    private void AddDoor(Connector connector, bool reverse)
    {
        var door = (Door)Instantiate(Door, new Vector3(200, Random.Range(1, 400) * 30, 1), transform.rotation);
        door.transform.SetParent(DungeonContainter.transform);
        PlaceDoor(connector, door.GetConnectors().FirstOrDefault(), reverse);
    }

    private void PlaceDoor(Connector ExitConnector, Connector DoorConnector, bool reverse)
    {
        var newModule = DoorConnector.transform.parent;
        var objectToConnectVector = -ExitConnector.transform.forward;
        var angle1 = Vector3.Angle(Vector3.forward, objectToConnectVector) * Mathf.Sign(objectToConnectVector.x);
        var angle2 = Vector3.Angle(Vector3.forward, DoorConnector.transform.forward) * Mathf.Sign(DoorConnector.transform.forward.x);
        newModule.RotateAround(DoorConnector.transform.position, Vector3.up, angle1 - angle2);
        var correctPosition = ExitConnector.transform.position - DoorConnector.transform.position;
        if (reverse)
        {
            newModule.Rotate(Vector3.up, 180f);
        }
        newModule.transform.position += correctPosition;
        Destroy(DoorConnector);
    }

    private void Connect(Connector startingObject, Connector ObjectToConnect)
    {
        var newModule = ObjectToConnect.transform.parent;
        var objectToConnectVector = -startingObject.transform.forward;

        var desiredAngle = Vector3.Angle(Vector3.forward, -startingObject.transform.forward) * Mathf.Sign(-startingObject.transform.forward.x);
        var actualAngle = Vector3.Angle(Vector3.forward, ObjectToConnect.transform.forward) * Mathf.Sign(ObjectToConnect.transform.forward.x);

        newModule.RotateAround(ObjectToConnect.transform.position, Vector3.up, desiredAngle - actualAngle);
        var correctPosition = startingObject.transform.position - ObjectToConnect.transform.position;
        newModule.transform.position += correctPosition;

        if (ObjectToConnect)
        {
            Destroy(startingObject.gameObject);
            Destroy(ObjectToConnect.gameObject);
        }
    }
    private IEnumerator SealEnds()
    {
        yield return new WaitForSecondsRealtime(1f);
        var ends = DungeonContainter.transform.GetComponentsInChildren<Connector>();
        foreach (var end in ends)
        {
            var newSeal = (Module)Instantiate(Seal, new Vector3(2, UnityEngine.Random.Range(1, 400) * 30, 1), transform.rotation);
            newSeal.transform.SetParent(DungeonContainter.transform);
            var secondModuleConnectors = newSeal.GetConnectors();
            var connectorToConnect = secondModuleConnectors.FirstOrDefault(x => x.startingConnector) ?? secondModuleConnectors.ElementAt(UnityEngine.Random.Range(0, secondModuleConnectors.Length));
            Connect(end, connectorToConnect);
        }
        GameController.IsDoneLoading = true;
    }

    private static TItem GetRandom<TItem>(TItem[] array)
    {
        return array[UnityEngine.Random.Range(0, array.Length)];
    }

    public void RenewIfCollided()
    {
        GameController.IsDoneLoading = false;
        StopAllCoroutines();

        foreach (Transform child in DungeonContainter.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        GameObject EnemyContainer = GameObject.Find("EnemiesContainer");

        foreach (Transform child in EnemyContainer.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        collided = false;
        //ClearLog();
        
        StartCoroutine(Starte(DungeonContainter));
    }
    // public void ClearLog()
    // {
    //     var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
    //     var type = assembly.GetType("UnityEditor.LogEntries");
    //     var method = type.GetMethod("Clear");
    //     method.Invoke(new object(), null);
    // }
    public void ChangeCollisionState()
    {
        this.collided = true;
    }


    public void NewDung()
    {
        foreach (Transform child in DungeonContainter.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        collided = false;
        StartCoroutine(Starte(DungeonContainter));
    }
}

