﻿using System.Collections;
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
    public Module Seal;

    private float waitTime = 0.3f;
    private GameObject dungeonContainter;

    private void Start()
    {
        StartCoroutine(Starte());
    }

    private IEnumerator Starte()
    {
        if (collided == false)
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
                    var newSelectedModule = GetRandomWithTag(Modules, randomType);
                    var newModule = (Module)Instantiate(newSelectedModule, new Vector3(2, Random.Range(1, 400) * 30, 1), transform.rotation);

                    newModule.transform.SetParent(dungeonContainter.transform);
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
                RenewIfCollided();
            }
            yield return new WaitForSeconds(waitTime);

            SealEnds();
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
        return array[UnityEngine.Random.Range(0, array.Length)];
    }
    private static Module GetRandomWithTag(IEnumerable<Module> modules, string tagToMatch)
    {
        var matchingModules = modules.Where(m => m.type.Contains(tagToMatch)).ToArray();
        return GetRandom(matchingModules);
    }

    private void Connect(Connector startingObject, Connector ObjectToConnect)
    {
        var newModule = ObjectToConnect.transform.parent;
        var forwardVector = -startingObject.transform.forward;
        var correctedRotation = Azimuth(forwardVector) - Azimuth(ObjectToConnect.transform.forward);
        newModule.RotateAround(ObjectToConnect.transform.position, Vector3.up, correctedRotation);
        var correctPosition = startingObject.transform.position - ObjectToConnect.transform.position;
        newModule.transform.position += correctPosition;

        if (ObjectToConnect)
        {
            Destroy(startingObject.gameObject);
            Destroy(ObjectToConnect.gameObject);
        }
    }
    private static float Azimuth(Vector3 vector)
    {
        return Vector3.Angle(Vector3.forward, vector) * Mathf.Sign(vector.x);
    }

    public void RenewIfCollided()
    {
        foreach (Transform child in dungeonContainter.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        collided = false;
        ClearLog();
        StopAllCoroutines();
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
}