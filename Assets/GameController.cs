using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static bool IsDoneLoading = false;
    public bool DoneLoading;
    public List<GameObject> Dungeons;
    public GameObject dung1;

    public bool enemiesDefeated = false;
    public GameObject Enemies;

    public Generator generator;
    public PlayerMovement playerObject;

    void Start()
    {
        DoneLoading = IsDoneLoading;
        //generator = GameObject.Find("GeneratorGO").GetComponent<Generator>();
        StartCoroutine(Begin());
    }

    IEnumerator Begin()
    {
        StartCoroutine(generator.Starte(dung1));
        yield return null;

    }
    // Update is called once per frame

    public void NewDungen()
    {

    }
    void Update()
    {

        if (Enemies.GetComponentsInChildren<Enemy>().Length == 0)
        {
            GameController.IsDoneLoading = false;
            generator.NewDung();
            playerObject.ResetPositionz();

        }

        DoneLoading = IsDoneLoading;
        if (IsDoneLoading == true)
        {
            //Debug.Log("Done");
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            playerObject.ResetPositionz();

        }

    }
}
