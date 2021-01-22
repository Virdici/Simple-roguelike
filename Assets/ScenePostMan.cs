using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class ScenePostMan : MonoBehaviour
{
    public InputField SeedText;
    public InputField Levels;
    public Slider EnemiesSlider;
    public Slider Rooms;
    public string seedFilled;
    public string seed;
    public int roomCount;
    public int enemiesMaxCount;
    public int levels;
    private static ScenePostMan original;
    private void Awake()
    {
        // DontDestroyOnLoad(this.gameObject);
        SeedText.characterLimit = 8;
        SeedText.text = RandomString(8);
        Levels.text = "3";
        
        if (original != this )
          {
              if( original != null )
                  Destroy(original.gameObject);
              DontDestroyOnLoad(gameObject);
              original = this;
          }
    }

    private void Update()
    {
        seed = SeedText.text;

        seedFilled = SeedText.text;
        enemiesMaxCount = (int)EnemiesSlider.value;
        roomCount = (int)Rooms.value;
        if (Levels.text == "0")
        {
            levels = 1;
        }
        if (Levels.text == "")
        {
            levels = 1;
        }

        try
        {
            levels = Int32.Parse(Levels.text);
        }
        catch (System.Exception) { }
    }

    private static System.Random random = new System.Random();
    public static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
    }
}
