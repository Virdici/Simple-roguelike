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
        StartCoroutine(Begin());
    }

    IEnumerator Begin()
    {
        StartCoroutine(generator.Starte(DungeonContainter));
        yield return null;

    }

    void Update()
    {
        if (Enemies.GetComponentsInChildren<Enemy>().Length == 0)
        {
            GameController.IsDoneLoading = false;
            generator.NewDung();
            playerObject.ResetPositionz();
            currentLevel++;
        }

        if (currentLevel == 3)
        {
            SceneManager.LoadSceneAsync(2);
        }

        DoneLoading = IsDoneLoading;
        if (IsDoneLoading == true)
        {
            //Debug.Log("Done");
        }
    }
}
