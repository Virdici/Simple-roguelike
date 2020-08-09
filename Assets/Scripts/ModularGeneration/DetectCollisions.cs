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

        Debug.Log(col.contacts[0].point);
        Debug.DrawRay(col.contacts[0].point, col.contacts[0].normal);
        generator.ChangeCollisionState();
    }
}
