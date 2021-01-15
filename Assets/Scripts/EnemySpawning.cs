using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;


public class EnemySpawning : MonoBehaviour
{
    public Enemy boss;
    public GameObject EnemyContainer;
    public GameObject BossContainer;
    public MeshFilter Filter;
    public int maxEnemies;
    public GameController gameController;

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
        gameController = GameObject.Find("GameController").GetComponent<GameController>();

    }
    public void Spawn()
    {
        Enemy enem;
        SpawnMesh = Filter.transform.GetComponent<MeshFilter>().mesh;
        vertices = SpawnMesh.vertices;

        for (int i = 0; i < Random.Range(1, maxEnemies); i++)
        {
            Vector3 randVex = transform.TransformPoint(vertices[Random.Range(0, vertices.Length)]);
            Vector3 rand = new Vector3(randVex.x, randVex.y - 1, randVex.z);

            if (isBossRoom)
            {
                enem = (Enemy)Instantiate(boss, rand, Quaternion.identity, BossContainer.transform);
                enem.index = transform.GetComponentInParent<Module>().index;
                transform.GetComponentInParent<Module>().Enemies.Add(enem);
            }
            else
            {
                // var randomEnemy = enemies[Random.Range(0, enemies.Length)]; 
                var randomEnemy = gameController.enemiesList[Random.Range(0, gameController.enemiesList.Length)];
                enem = (Enemy)Instantiate(randomEnemy, rand, Quaternion.identity, EnemyContainer.transform);
                enem.index = transform.GetComponentInParent<Module>().index;
                transform.GetComponentInParent<Module>().Enemies.Add(enem);
            }

        }
        placed = true;
    }


    public void SpawnSix()
    {
        Enemy enem;
        SpawnMesh = Filter.transform.GetComponent<MeshFilter>().mesh;
        vertices = SpawnMesh.vertices;
            Vector3 randVex = transform.TransformPoint(vertices[Random.Range(0, vertices.Length)]);
            Vector3 rand = new Vector3(randVex.x, randVex.y - 1, randVex.z);

        for (int i = 0; i <= Random.Range(1, maxEnemies); i++)
        {
            Debug.Log(rand);
            if (isBossRoom)
            {
                enem = (Enemy)Instantiate(boss, rand, Quaternion.identity, BossContainer.transform);
                enem.index = transform.GetComponentInParent<Module>().index;
                transform.GetComponentInParent<Module>().Enemies.Add(enem);
            }
            else
            {
                // var randomEnemy = enemies[Random.Range(0, enemies.Length)]; 
                var randomEnemy = gameController.enemiesList[Random.Range(0, gameController.enemiesList.Length)];
                enem = (Enemy)Instantiate(randomEnemy, rand, Quaternion.identity, EnemyContainer.transform);
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
            if (GetComponent<Module>().index == 16)
            {
                SpawnSix();
            }
            else
            {
                Spawn();

            }
        }
    }
}
