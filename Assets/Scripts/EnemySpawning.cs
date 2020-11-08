using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawning : MonoBehaviour
{
    public Enemy enemy;
    public float x, y, z;
    public Vector3 RoomSize;
    public GameObject EnemyContainer;
    public MeshFilter Filter;
    public int maxEnemies;


    private GameObject host;
    private bool placed;
    private Vector3[] vertices;
    private Mesh SpawnMesh;
    void Awake()
    {
        if (transform.GetComponentInParent<Module>().index != 0)
        {
            // EnemyContainer = GameObject.Find("EnemiesContainer");
            // host = transform.GetComponent<Module>().gameObject;
            // RoomSize = host.GetComponent<Renderer>().bounds.size;
            // RoomSize = RoomSize - new Vector3(10, 2, 10);
            // x = RoomSize.x / 2;
            // z = RoomSize.z / 2;
            SpawnMesh = Filter.transform.GetComponent<MeshFilter>().mesh;
            Spawn();
        }
        EnemyContainer = GameObject.Find("EnemiesContainer");

    }

    public void Spawn()
    {
        // host = transform.GetComponent<Module>().gameObject;
        // RoomSize = host.GetComponent<Renderer>().bounds.size;
        // RoomSize = RoomSize - new Vector3(6, 2, 6);
        // x = RoomSize.x / 2;
        // z = RoomSize.z / 2;
        Enemy enem;

        // Vector3 HostPosition = new Vector3(host.transform.position.x, host.transform.position.y, host.transform.position.z);
        // Vector3 randomPosition = HostPosition + new Vector3(Random.Range(-x, x), 1f, Random.Range(-z, z));
        
        // for (int i = 0; i < Random.Range(1, 2); i++)
        // {
        //     enem = (Enemy)Instantiate(enemy, HostPosition + new Vector3(Random.Range(-x, x), 1f, Random.Range(-z, z)), Quaternion.identity);
        //     enem.transform.SetParent(EnemyContainer.transform);
        //     enem.index = transform.GetComponentInParent<Module>().index;
        //     transform.GetComponentInParent<Module>().Enemies.Add(enem);
        // }

        SpawnMesh = Filter.transform.GetComponent<MeshFilter>().mesh;
        vertices = SpawnMesh.vertices;
         
         for (int i = 0; i <= Random.Range(2,maxEnemies); i++)
         {
            Vector3 randVex = transform.TransformPoint(vertices[Random.Range(0,vertices.Length)]);
            Vector3 rand = new Vector3(randVex.x,randVex.y-1,randVex.z);
            
            enem = (Enemy)Instantiate(enemy, rand, Quaternion.identity);
            enem.transform.SetParent(EnemyContainer.transform);
            enem.index = transform.GetComponentInParent<Module>().index;
            transform.GetComponentInParent<Module>().Enemies.Add(enem);  
         }

        placed = true;
    }

    private void Update()
    {
        if (GameController.IsDoneLoading && !placed && transform.GetComponentInParent<Module>().index != 0)
        {
            Spawn();
        }
        

    }

}
