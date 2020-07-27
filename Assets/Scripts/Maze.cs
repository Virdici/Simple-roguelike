using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WeightedWall
{
    public GameObject WallPrefab;
    public int Weight;

    public WeightedWall(GameObject wallPrefab, int weight)
    {
        WallPrefab = wallPrefab;
        Weight = weight;
    }
}
public class Maze : MonoBehaviour
{
    public GameObject WallPrefab;
    public GameObject Floor;

    public int meshWidth;
    public int meshHeight;

    public float waitTime;

    private Random rnd;

    private Tile[] tiles;

    private List<GameObject> HorizontalWalls = new List<GameObject>();
    private List<GameObject> VerticalWalls = new List<GameObject>();
    private List<GameObject> OutterWalls = new List<GameObject>();
    private List<GameObject> Floors = new List<GameObject>();
    private List<WeightedWall> WeightedWalls = new List<WeightedWall>();

    void StartT()
    {
        tiles = new Tile[meshHeight * meshWidth];
        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i] = new Tile();
        }

        for (int z = 1; z <= meshHeight; z++)
        {
            for (int x = 1; x <= meshWidth; x++)
            {
                Floors.Add(Instantiate(Floor, new Vector3(x * 4, -1f, z * 4), Quaternion.Euler(0, 0, 0)));
            }
        }

        //góra i duł
        for (int z = 1; z <= meshHeight; z++)
        {
            for (int x = 1; x <= meshWidth; x++)
            {
                if (z == 1)
                {
                    OutterWalls.Add(Instantiate(WallPrefab, new Vector3(x * 4, 0, z * 4 - 2f), Quaternion.Euler(0, 0, 0)));
                }

                if (z == meshHeight)
                {
                    OutterWalls.Add(Instantiate(WallPrefab, new Vector3(x * 4, 0, z * 4 + 2f), Quaternion.Euler(0, 0, 0)));
                }
            }
        }

        //lewo i prawo
        for (int x = 1; x <= meshWidth; x++)
        {
            for (int z = 1; z <= meshHeight; z++)
            {
                if (x == 1)
                {
                    OutterWalls.Add(Instantiate(WallPrefab, new Vector3(x * 4 - 2f, 0, z * 4), Quaternion.Euler(0, 90, 0)));
                }
                if (x == meshWidth)
                {
                    OutterWalls.Add(Instantiate(WallPrefab, new Vector3(x * 4 + 2f, 0, z * 4), Quaternion.Euler(0, 90, 0)));
                }
            }
        }

        //StartCoroutine(InnerWallsGen());

    }

    IEnumerator InnerWallsGen()
    {
        //horizontal walls
        for (int z = 1; z < meshHeight; z++)
        {
            for (int x = 1; x <= meshWidth; x++)
            {
                WeightedWall newWall = new WeightedWall(
                    Instantiate(WallPrefab, new Vector3(x * 4, 0, z * 4 + 2f), Quaternion.Euler(0, 0, 0)), Random.Range(1, 8));

                WeightedWalls.Add(newWall);

                //HorizontalWalls.Add(Instantiate(WallPrefab, new Vector3(x * 4, 0, z * 4 + 2f), Quaternion.Euler(0, 0, 0)));
                yield return new WaitForSeconds(waitTime);
            }
        }
        //vertical walls
        for (int z = 1; z <= meshHeight; z++)
        {
            for (int x = 1; x < meshWidth; x++)
            {
                WeightedWall newWall = new WeightedWall(
                    Instantiate(WallPrefab, new Vector3(x * 4 + 2f, 0, z * 4), Quaternion.Euler(0, 90, 0)), Random.Range(1, 8));

                WeightedWalls.Add(newWall);
                //VerticalWalls.Add(Instantiate(WallPrefab, new Vector3(x * 4 + 2f, 0, z * 4), Quaternion.Euler(0, 90, 0)));
                yield return new WaitForSeconds(waitTime);
            }
        }
    }

    IEnumerator GridReset()
    {
        for (int i = 0; i < HorizontalWalls.Count; i++)
        {
            Destroy(HorizontalWalls[i]);

        }
        for (int i = 0; i < VerticalWalls.Count; i++)
        {
            Destroy(VerticalWalls[i]);

        }
        for (int i = 0; i < OutterWalls.Count; i++)
        {
            Destroy(OutterWalls[i]);
        }
        for (int i = 0; i < Floors.Count; i++)
        {
            Destroy(Floors[i]);
        }
        for (int i = 0; i < WeightedWalls.Count; i++)
        {
            Destroy(WeightedWalls[i].WallPrefab);

        }
        yield return new WaitForSeconds(0f);

    }

    IEnumerator RemoveUp()
    {

        for (int j = 1; j <= 4; j++)
        {
            for (int i = 0; i < WeightedWalls.Count; i++)
            {
                if (WeightedWalls[i].Weight == j)
                {
                    Destroy(WeightedWalls[i].WallPrefab);
                    yield return new WaitForSeconds(0.01f);
                }

            }
        }

        //int toDel = 89;
        //toDel -= 1;
        //Destroy(HorizontalWalls[toDel]);
        //Destroy(VerticalWalls[toDel]);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartT();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StopAllCoroutines();
            StartCoroutine(GridReset());
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            StartCoroutine(InnerWallsGen());
        }

        if (Input.GetKeyDown(KeyCode.Delete))
        {
            StartCoroutine(RemoveUp());
        }
    }
}
