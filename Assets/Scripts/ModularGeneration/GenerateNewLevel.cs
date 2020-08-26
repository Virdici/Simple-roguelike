using System.Collections;
using UnityEngine;

public class GenerateNewLevel : MonoBehaviour
{
    public bool DidCollide = false;
    public Generator generator;

    private void Awake()
    {
        //generator = GameObject.FindObjectOfType<Generator>();
    }
    private void OnCollisionExit(Collision col)
    {
        //new generation on collision
        GameController.IsDoneLoading = false;
        Debug.Log("collision detected" + col.gameObject.name);
        //generator.RenewIfCollided();
        generator.NewDung();
    }
    private void OnCollisionEnter(Collision col)
    {
        
    }
}
