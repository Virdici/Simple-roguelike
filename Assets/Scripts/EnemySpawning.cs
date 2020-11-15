using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class EnemySpawning : MonoBehaviour
{
    public Enemy enemy;
    public Enemy boss;
    public GameObject EnemyContainer;
    public GameObject BossContainer;
    public MeshFilter Filter;
    public int maxEnemies;

    private bool placed;
    private Vector3[] vertices;
    private Mesh SpawnMesh;
    public bool isBossRoom = false;
    void Awake()
    {
        if (transform.GetComponentInParent<Module>().index != 0)
        {
            SpawnMesh = Filter.transform.GetComponent<MeshFilter>().mesh;
            Spawn();
        }
        EnemyContainer = GameObject.Find("EnemiesContainer");

    }
    public void Spawn()
    {
        Enemy enem;
        SpawnMesh = Filter.transform.GetComponent<MeshFilter>().mesh;
        vertices = SpawnMesh.vertices;

        for (int i = 0; i <= Random.Range(0, maxEnemies); i++)
        {
            Vector3 randVex = transform.TransformPoint(vertices[Random.Range(0, vertices.Length)]);
            Vector3 rand = new Vector3(randVex.x, randVex.y - 1, randVex.z);

            if (isBossRoom)
            { 
                enem = (Enemy)Instantiate(boss, rand, Quaternion.identity);
                enem.transform.SetParent(BossContainer.transform);
                enem.index = transform.GetComponentInParent<Module>().index;
                transform.GetComponentInParent<Module>().Enemies.Add(enem);
            }
            else
            { 
                enem = (Enemy)Instantiate(enemy, rand, Quaternion.identity);
                enem.transform.SetParent(EnemyContainer.transform);
                enem.index = transform.GetComponentInParent<Module>().index;
                transform.GetComponentInParent<Module>().Enemies.Add(enem);
            }
            
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
