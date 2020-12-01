using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScenePostMan : MonoBehaviour
{
    public InputField text;
    public string seed;
    private void Awake()
    {
        
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update() {
        seed = text.text;
    }
}
