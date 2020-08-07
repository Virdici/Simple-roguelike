using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    public Enemy enemy;
    public float x, y, z;
    public Vector3 RoomSize;
    public GameObject EnemyContainer;

    private GameObject host;
    void Start()
    {
        if (transform.GetComponentInParent<Module>().index != 0)
        {
            EnemyContainer = GameObject.Find("EnemiesContainer");
            host = transform.GetComponentInParent<Module>().gameObject;
            RoomSize = host.GetComponent<Renderer>().bounds.size;
            RoomSize = RoomSize - new Vector3(10, 2, 10);
            x = RoomSize.x / 2;
            z = RoomSize.z / 2;
            Spawn();
        } 
    }

    public void Spawn()
    {
        EnemyContainer = GameObject.Find("EnemiesContainer");
        host = transform.GetComponentInParent<Module>().gameObject;
        RoomSize = host.GetComponent<Renderer>().bounds.size;
        RoomSize = RoomSize - new Vector3(6, 2, 6);
        x = RoomSize.x / 2;
        z = RoomSize.z / 2;
        Enemy enem;

        Vector3 HostPosition = new Vector3(host.transform.position.x, host.transform.position.y, host.transform.position.z);


        for (int i = 0; i < Random.Range(1, 3); i++)
        {
            enem = (Enemy)Instantiate(enemy, HostPosition + new Vector3(Random.Range(-x, x), 1, Random.Range(-z, z)), Quaternion.identity);
            enem.transform.SetParent(EnemyContainer.transform);
            transform.GetComponentInParent<Module>().Enemies.Add(enem);
        }

    }
}
