using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class ScenePostMan : MonoBehaviour
{
    public InputField SeedText;
    public Slider EnemiesSlider;
    public Slider Rooms;
    public string seedFilled;
    public string seed;
    public int roomCount;
    public int enemiesMaxCount;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        SeedText.characterLimit = 8;
        SeedText.text = RandomString(8);
    }

    private void Update()
    {
        seed = SeedText.text;
        seedFilled = SeedText.text;
        enemiesMaxCount = (int)EnemiesSlider.value;
        roomCount = (int)Rooms.value;
    }

    private static System.Random random = new System.Random();
    public static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
    }
}
