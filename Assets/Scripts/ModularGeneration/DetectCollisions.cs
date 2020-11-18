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

        Debug.DrawRay(col.contacts[0].point, col.contacts[0].normal);
        
        if (col.gameObject.layer != 12 && col.gameObject.layer != 11)
        {
            //Debug.Log(col.gameObject.name);
            generator.ChangeCollisionState();
        }
    }

    
}
