using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Reflection;

public class Generator : MonoBehaviour
{
    GameObject firstModule;
    GameObject secondModule;

    public Module[] Modules;
    public Module startingModule;
    public int size = 1;
    public bool collided;
    public bool done;
    public Module Seal;

    private float waitTime = 4f;
    private GameObject levelContainer;
    private GameObject currentLevel;
    private GameObject dungeonContainter;

    private void Start()
    {
        //StartCoroutine(Generate());
        currentLevel = GameObject.Find("Level1");
        StartCoroutine(StartGeneration());
        
    }

    IEnumerator StartGeneration()
    {
        dungeonContainter = GameObject.Find("DungeonContainer");


        for (int i = 0; i < 5; i++)
        {
            currentLevel = dungeonContainter.transform.GetChild(i).gameObject;
            StartCoroutine(Generate(currentLevel));
            
            done = false;
        }

        yield return new WaitForSeconds(waitTime);

    }

    private IEnumerator Generate(GameObject level)
    {
        if (collided == false && done == false)
        {
            dungeonContainter = GameObject.Find("DungeonContainer");

            var firstModule = (Module)Instantiate(startingModule, transform.position, transform.rotation);
            firstModule.transform.SetParent(level.transform);
            firstModule.transform.position = level.transform.position;
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
                    newModule.transform.SetParent(level.transform);
                    var secondModuleConnectors = newModule.GetConnectors();
                    var connectorToConnect = secondModuleConnectors.FirstOrDefault(x => x.startingConnector) ?? secondModuleConnectors.ElementAt(Random.Range(0, secondModuleConnectors.Length));
                     Connect(selectedConnector, connectorToConnect);

                    allExits.AddRange(secondModuleConnectors.Where(e => e != connectorToConnect));
                }
                availableConnectors = allExits;
            }
            yield return new WaitForSeconds(waitTime);
            if (collided == true)
            {
                RenewIfCollided(level);
            }
            yield return new WaitForSeconds(waitTime);

            SealEnds();

            done = true;
            yield return new WaitUntil(() => done = true);
        }
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
            var secondModuleConnectors = newSeal.GetConnectors();
            var connectorToConnect = secondModuleConnectors.FirstOrDefault(x => x.startingConnector) ?? secondModuleConnectors.ElementAt(UnityEngine.Random.Range(0, secondModuleConnectors.Length));
            Connect(end, connectorToConnect);
        }
    }

    private static TItem GetRandom<TItem>(TItem[] array)
    {
        return array[Random.Range(0, array.Length)];
    }

    public void RenewIfCollided(GameObject level)
    {
        foreach (Transform child in level.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        collided = false;
        ClearLog();
        //StopAllCoroutines();
        StartCoroutine(Generate(currentLevel));
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
}
