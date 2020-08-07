using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    public bool DidCollide = false;
    Generator generator;

    private void Awake()
    {
        Physics.IgnoreLayerCollision(9, 11);
        generator = GameObject.FindObjectOfType<Generator>();
    }
    private void OnCollisionEnter(Collision col)
    {
        generator.ChangeCollisionState();
    }
}
