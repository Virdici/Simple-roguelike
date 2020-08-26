using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        StartCoroutine(LoadAsync());
    }
    public void ExitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    IEnumerator LoadAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);


        while (GameController.IsDoneLoading != true)
        {
            Debug.Log(operation.progress);
            yield return null;
        }
    }
}

