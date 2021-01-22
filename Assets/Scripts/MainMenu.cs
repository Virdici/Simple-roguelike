using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class MainMenu : MonoBehaviour
{
    public ScenePostMan postMan;
    public bool isPauseMenu;
    private void Update() {
        Cursor.visible = true;
    }
    public void StartGame()
    {
        StartCoroutine(LoadAsync());
    }
    public void ExitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        if(!isPauseMenu)
        Time.timeScale = 1.0f;

    }

    public void BackToMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }

    IEnumerator LoadAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);


        while (GameController.IsDoneLoading != true)
        {
            // Debug.Log(operation.progress);
            yield return null;
        }
    }
}

