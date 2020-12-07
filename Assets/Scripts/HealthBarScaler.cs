using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarScaler : MonoBehaviour
{

    public RectTransform RectTransform;
    void Update()
    {
        RectTransform.localScale = new Vector3((float)(Screen.width / 100) * (float)(Screen.width / 1920.0), (float)(Screen.height / 200) * (float)(Screen.height / 1080.0), 1);
    }
}
