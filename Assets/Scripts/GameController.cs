﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public ScenePostMan postMan;
    public static bool IsDoneLoading = false;
    public bool DoneLoading;
    public GameObject dungeonContainter;

    public bool enemiesDefeated = false;
    public GameObject Enemies;

    public Generator generator;
    public PlayerMovement playerObject;
    public int currentLevel = 1;
    public int maxLevel = 5;
    

    void Start()
    {
        dungeonContainter = GameObject.Find("DungeonContainer");
        Enemies = GameObject.Find("EnemiesContainer");
        generator = GetComponent<Generator>();
        playerObject = GameObject.Find("PlayerObject").GetComponent<PlayerMovement>();
        postMan = GameObject.Find("Sender").GetComponent<ScenePostMan>();

        DoneLoading = IsDoneLoading;
        // Physics.IgnoreLayerCollision(11, 16);
        // Physics.IgnoreLayerCollision(12, 16);
        // Physics.IgnoreLayerCollision(8, 16);
        // Physics.IgnoreLayerCollision(13, 16);
        // Physics.IgnoreLayerCollision(9, 16);
        Begin();
    }

    void Begin()
    {
        StartCoroutine(generator.Starte(dungeonContainter));


    }

    void Update()
    {
        // if (Enemies.GetComponentsInChildren<Enemy>().Length == 0 && DoneLoading)
        // {
        //     GameController.IsDoneLoading = false;
        //     generator.NewDung();
        //     playerObject.ResetPositionz();
        //     currentLevel++;
        // }

        

        if (currentLevel == 3)
        {
            // SceneManager.LoadSceneAsync(2);
        }

        DoneLoading = IsDoneLoading;
       
    }
}
