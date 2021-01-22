using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    public bool DidCollide = false;
    Generator generator;


    private void Awake()
    {
        //Physics.IgnoreLayerCollision(8, 11);
        generator = GameObject.FindObjectOfType<Generator>();
    }
    private void OnCollisionEnter(Collision col)
    {


        // if (col.gameObject.layer == 16)
        if (col.gameObject.layer != 12 && col.gameObject.layer != 11 && col.gameObject.layer != 18)
        {
            Debug.Log(col.gameObject.name);
            generator.ChangeCollisionState();
            // generator.RenewIfCollided();
        }
    }

    
}
