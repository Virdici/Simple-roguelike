using System.Collections;
using UnityEngine;

public class GenerateNewLevel : MonoBehaviour
{
    public bool DidCollide = false;
    Generator generator;

    private void Awake()
    {
        generator = GameObject.FindObjectOfType<Generator>();
    }

    private void OnCollisionStay(Collision col)
    {
        //new generation on collision
        Debug.Log("collision detected" + col.contacts[0].point);
        //StartCoroutine(Start());
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);
        generator.StartNewGeneration();
    }
}
