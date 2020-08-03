using System.Collections;
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
    public Module Door;
    public GameObject DoorSwitch;

    private float waitTime = 0.3f;
    private GameObject dungeonContainter;

    private void Start()
    {
        StartCoroutine(Starte());
    }

    private IEnumerator Starte()
    {
        
        dungeonContainter = GameObject.Find("DungeonContainer");

        var firstModule = (Module)Instantiate(startingModule, new Vector3(0, 0, 0), transform.rotation);
        firstModule.transform.SetParent(dungeonContainter.transform);
        //AddSwitch(firstModule);
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
                if (newModule.GetTypeName() == "room")
                {
                    AddSwitch(newModule);
                }

                newModule.transform.SetParent(dungeonContainter.transform);
                var secondModuleConnectors = newModule.GetConnectors();
                var connectorToConnect = secondModuleConnectors.FirstOrDefault(x => x.startingConnector) ?? secondModuleConnectors.ElementAt(Random.Range(0, secondModuleConnectors.Length));

                var door = (Module)Instantiate(Door, new Vector3(200, Random.Range(1, 400) * 30, 1), transform.rotation);
                door.transform.SetParent(dungeonContainter.transform);
                AddDoor(selectedConnector, door.GetConnectors().FirstOrDefault());

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
        SealEnds();
    }

    private void AddSwitch(Module module)
    {
        var DoorSwitchh = Instantiate(DoorSwitch, new Vector3(2, Random.Range(1, 400) * 30, 10000), transform.rotation);
        DoorSwitchh.transform.SetParent(module.transform);
        DoorSwitchh.transform.position = new Vector3(5, 3, 0);
    }

    private void AddDoor(Connector ExitConnector, Connector DoorConnector)
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

        if (ObjectToConnect)
        {
            Destroy(startingObject.gameObject);
            Destroy(ObjectToConnect.gameObject);
        }
    }
    private void SealEnds()
    {
        var ends = FindObjectsOfType<Connector>();
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
        StopAllCoroutines();
        foreach (Transform child in dungeonContainter.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        collided = false;
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

    public void StartNewGeneration()
    {
        StopAllCoroutines();
        StartCoroutine(Starte());
    }
}
