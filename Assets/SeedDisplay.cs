using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeedDisplay : MonoBehaviour
{
    private Text text;
    public ScenePostMan postMan;
    void Start()
    {
        text = GetComponentInParent<Text>();
        postMan = GameObject.Find("Sender").GetComponent<ScenePostMan>();
        text.text = postMan.seedFilled.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
