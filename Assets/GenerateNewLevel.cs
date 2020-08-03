using UnityEngine;

public class GenerateNewLevel : MonoBehaviour
{
    public bool DidCollide = false;
    Generator generator;

    private void Awake()
    {
        generator = GameObject.FindObjectOfType<Generator>();
    }

    private void OnCollisionEnter(Collision col)
    {
        //new generation on collision
        Debug.Log("collision detected" + col.contacts[0].point);
        generator.RenewIfCollided();
        
    }
}
