using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    public bool DidCollide = false;
    Generator generator;

    private void Awake()
    {
        generator = GameObject.FindObjectOfType<Generator>();
    }

    private void OnCollisionStay(Collision collision)
    {
        Physics.IgnoreLayerCollision(8, 10);

    }
    private void OnCollisionEnter(Collision col)
    {
        Debug.Log("collision detected" + col.contacts[0].point);
        generator.ChangeCollisionState();
    }
}
