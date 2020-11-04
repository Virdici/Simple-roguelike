using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static bool IsDoneLoading = false;
    public bool DoneLoading;
    public GameObject DungeonContainter;

    public bool enemiesDefeated = false;
    public GameObject Enemies;

    public Generator generator;
    public PlayerMovement playerObject;
    public int currentLevel = 1;
    public int maxLevel = 5;

    void Start()
    {
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
        StartCoroutine(generator.Starte(DungeonContainter));


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

        if (Input.GetKeyDown(KeyCode.R))
        {
            generator.RenewIfCollided();
        }

        if (currentLevel == 3)
        {
            SceneManager.LoadSceneAsync(2);
        }

        DoneLoading = IsDoneLoading;
       
    }
}
