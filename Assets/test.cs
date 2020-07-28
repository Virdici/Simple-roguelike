using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public Connector connectorAsTarget;
    public Connector ModuleConnector;

    void Start()
    {
        Connect(connectorAsTarget, ModuleConnector);
    }
    private void Connect(Connector startingObject, Connector ObjectToConnect)
    {
        var newModule = ObjectToConnect.transform.parent;
        // Debug.Log(newModule.transform.forward);
        // Debug.Log(ObjectToConnect.transform.forward);

        // ObjectToConnect.transform.rotation = startingObject.transform.rotation;
        // var childRotation = ObjectToConnect.transform.rotation;
        // ObjectToConnect.transform.Rotate(Vector3.up, 180);
        //// ObjectToConnect.transform.parent.transform.rotation = childRotation;
        // newModule.rotation = ObjectToConnect.transform.rotation;
        //ObjectToConnect.transform.rotation = startingObject.transform.rotation;
        //ObjectToConnect.transform.Rotate(Vector3.up, 180);


        //newModule.RotateAround(ObjectToConnect.transform.position, angle);
        // newModule.LookAt(ObjectToConnect.transform.position);



        //ObjectToConnect.transform.rotation = startingObject.transform.rotation;
        //ObjectToConnect.transform.Rotate(Vector3.up, 180);

        //var point = ObjectToConnect.transform.position;
        //var targerDir = ObjectToConnect.transform.position - newModule.position;
        //var angle = Vector3.Angle(targerDir, ObjectToConnect.transform.position);
        //var angle1 = Vector3.Angle(targerDir, ObjectToConnect.transform.position);
        ////Debug.Log(startingObject.transform.forward);
        //Debug.Log(ObjectToConnect.transform.forward);
        //Debug.Log(targerDir);
        //Debug.Log(angle);
        //newModule.transform.RotateAround(point, Vector3.up, angle);

        var correctPosition = startingObject.transform.position - ObjectToConnect.transform.position;
        newModule.transform.position += correctPosition;
        var objectToConnectVector = -startingObject.transform.forward;
        var angle1 = Vector3.Angle(Vector3.forward, objectToConnectVector);
        var angle2 = Vector3.Angle(Vector3.forward, ObjectToConnect.transform.forward);
        newModule.RotateAround(ObjectToConnect.transform.position, Vector3.up, angle1-angle2);




        //float singleStep = 0.01f * Time.deltaTime;
        //var targetDirection = startingObject.transform.position - ObjectToConnect.transform.position;
        //var newDirection = Vector3.RotateTowards(-startingObject.transform.forward, targetDirection, singleStep, 0f);

        //newModule.rotation = Quaternion.LookRotation(newDirection);

        //var forwardVector = -startingObject.transform.forward;
        //var correctedRotation = Azimuth(forwardVector) - Azimuth(ObjectToConnect.transform.forward);
        //newModule.RotateAround(ObjectToConnect.transform.position, Vector3.up, correctedRotation);









        //if (ObjectToConnect)
        //{
        //    Destroy(startingObject.gameObject);
        //    Destroy(ObjectToConnect.gameObject);
        //}
    }

    float gerAngle(Connector ObjectToConnect)
    {
        if (ObjectToConnect.transform.forward == new Vector3(1,1,1))
        {

        }
        return 1f;
    }
    // Update is called once per frame
    private static float Azimuth(Vector3 vector)
    {
        return Vector3.Angle(Vector3.forward, vector) * Mathf.Sign(vector.x);
    }
}
