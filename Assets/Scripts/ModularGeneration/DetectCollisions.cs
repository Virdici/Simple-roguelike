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

    private void OnCollisionStay(Collision collision)
    {
        Physics.IgnoreLayerCollision(9, 11);
    }
    private void OnCollisionEnter(Collision col)
    {
        Physics.IgnoreLayerCollision(9, 11);
        generator.ChangeCollisionState();
    }
}
