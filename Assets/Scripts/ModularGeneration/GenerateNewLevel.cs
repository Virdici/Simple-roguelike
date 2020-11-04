using System.Collections;
using UnityEngine;

public class GenerateNewLevel : MonoBehaviour
{
    public bool DidCollide = false;
    public Generator generator;
   
    private void OnCollisionExit(Collision col)
    {
        //new generation on collision
        GameController.IsDoneLoading = false;
        Debug.Log("collision detected" + col.gameObject.name);
        generator.NewDung();
    }

}
