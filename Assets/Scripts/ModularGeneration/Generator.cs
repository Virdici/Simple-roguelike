﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;

public class Generator : MonoBehaviour
{
    GameObject firstModule;
    GameObject secondModule;

    public Module[] Modules;
    public Module startingModule;
    public int size = 1;
    public bool collided;
    public Module Seal;
    public Door Door;

    private float waitTime = 0.03f;
    private GameObject dungeonContainter;
    private bool FinishedPlacingRooms;
    private int index = 0;
    private void Start()
    {
        StartCoroutine(Starte());
    }

    private IEnumerator Starte()
    {
        dungeonContainter = GameObject.Find("DungeonContainer");

        var firstModule = (Module)Instantiate(startingModule, new Vector3(0, 0, 0), transform.rotation);
        firstModule.transform.SetParent(dungeonContainter.transform);
        var availableConnectors = new List<Connector>(firstModule.GetConnectors());

        for (int i = 0; i < size; i++)
        {
            var allExits = new List<Connector>();
            foreach (var selectedConnector in availableConnectors)
            {
                var randomType = selectedConnector.allowedTypes.ElementAt(Random.Range(0, selectedConnector.allowedTypes.Length));

                var matchingModules = Modules.Where(m => m.type.Contains(randomType)).ToArray();
                var newSelectedModule = GetRandom(matchingModules);
                var newModule = (Module)Instantiate(newSelectedModule, new Vector3(2, Random.Range(1, 400) * 30, 1), transform.rotation);
                newModule.transform.SetParent(dungeonContainter.transform);
                var secondModuleConnectors = newModule.GetConnectors();
                var connectorToConnect = secondModuleConnectors.FirstOrDefault(x => x.startingConnector) ?? secondModuleConnectors.ElementAt(Random.Range(0, secondModuleConnectors.Length));

                if (newModule.GetTypeName() == "room")
                {
                    index++;
                    newModule.index = index;
                }

                Connect(selectedConnector, connectorToConnect);

                allExits.AddRange(secondModuleConnectors.Where(e => e != connectorToConnect));
            }
            availableConnectors = allExits;
        }
        yield return new WaitForSeconds(waitTime);
        if (collided == true)
        {
            RenewIfCollided();
        }
        yield return new WaitForSeconds(waitTime);
        FinishedPlacingRooms = true;
        SealEnds();
    }

    private void AddDoor(Connector connector)
    {
        var door = (Door)Instantiate(Door, new Vector3(200, Random.Range(1, 400) * 30, 1), transform.rotation);
        door.transform.SetParent(dungeonContainter.transform);
        door.SetIndex(index);
        PlaceDoor(connector, door.GetConnectors().FirstOrDefault());
    }

    private void PlaceDoor(Connector ExitConnector, Connector DoorConnector)
    {
        var newModule = DoorConnector.transform.parent;
        var objectToConnectVector = -ExitConnector.transform.forward;
        var angle1 = Vector3.Angle(Vector3.forward, objectToConnectVector) * Mathf.Sign(objectToConnectVector.x);
        var angle2 = Vector3.Angle(Vector3.forward, DoorConnector.transform.forward) * Mathf.Sign(DoorConnector.transform.forward.x);
        newModule.RotateAround(DoorConnector.transform.position, Vector3.up, angle1 - angle2);
        var correctPosition = ExitConnector.transform.position - DoorConnector.transform.position;
        newModule.transform.position += correctPosition;
        Destroy(DoorConnector);
    }

    private void Connect(Connector startingObject, Connector ObjectToConnect)
    {
        var newModule = ObjectToConnect.transform.parent;
        var objectToConnectVector = -startingObject.transform.forward;
        var angle1 = Vector3.Angle(Vector3.forward, objectToConnectVector) * Mathf.Sign(objectToConnectVector.x);
        var angle2 = Vector3.Angle(Vector3.forward, ObjectToConnect.transform.forward) * Mathf.Sign(ObjectToConnect.transform.forward.x);
        newModule.RotateAround(ObjectToConnect.transform.position, Vector3.up, angle1 - angle2);
        var correctPosition = startingObject.transform.position - ObjectToConnect.transform.position;
        newModule.transform.position += correctPosition;

        var SelectedConnectorParent = startingObject.transform.GetComponentInParent<Module>();
        if (SelectedConnectorParent.GetTypeName() == "room" && !FinishedPlacingRooms)
            AddDoor(startingObject);

        SelectedConnectorParent = ObjectToConnect.transform.GetComponentInParent<Module>();
        if (SelectedConnectorParent.GetTypeName() == "room" && !FinishedPlacingRooms)
            AddDoor(ObjectToConnect);

        if (ObjectToConnect)
        {
            Destroy(startingObject.gameObject);
            Destroy(ObjectToConnect.gameObject);
        }
    }
    private void SealEnds()
    {
        var ends = dungeonContainter.transform.GetComponentsInChildren<Connector>();
        foreach (var end in ends)
        {
            var newSeal = (Module)Instantiate(Seal, new Vector3(2, UnityEngine.Random.Range(1, 400) * 30, 1), transform.rotation);
            newSeal.transform.SetParent(dungeonContainter.transform);
            var secondModuleConnectors = newSeal.GetConnectors();
            var connectorToConnect = secondModuleConnectors.FirstOrDefault(x => x.startingConnector) ?? secondModuleConnectors.ElementAt(UnityEngine.Random.Range(0, secondModuleConnectors.Length));
            Connect(end, connectorToConnect);
        }
    }

    private static TItem GetRandom<TItem>(TItem[] array)
    {
        return array[UnityEngine.Random.Range(0, array.Length)];
    }

    public void RenewIfCollided()
    {
        index = 0;
        StopAllCoroutines();
        foreach (Transform child in dungeonContainter.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        GameObject EnemyContainer = GameObject.Find("EnemiesContainer");

        foreach (Transform child in EnemyContainer.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        collided = false;
        FinishedPlacingRooms = false;
        ClearLog();
        StartCoroutine(Starte());
    }
    public void ClearLog()
    {
        var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
        var type = assembly.GetType("UnityEditor.LogEntries");
        var method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
    }
    public void ChangeCollisionState()
    {
        this.collided = true;
    }


    public void NewDung()
    {
        foreach (Transform child in dungeonContainter.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        collided = false;
        StartCoroutine(Starte());
    }
}
