using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MapManager : MonoBehaviour
{

    [Range(3, 25)]
    public int sizeOfMap;

    public GameObject rockTilePrefab;

    public GameObject grassTilePrefab;

    public GameObject waterTilePrefab;

    public GameObject sandTilePrefab;

    public GameObject tileParent;
    public float x;
    public float y;
    public float z;
    public Vector3 fieldPosition;
    public Quaternion fieldRotation = Quaternion.identity;
    public int numberOfTypesOfTiles;
    public Dictionary<Vector2, int> posAndIds = new Dictionary<Vector2, int>();
    public Dictionary<int, TileScript> tiles = new Dictionary<int, TileScript>();

    [Range(1, 10)]
    public int scale;

    [Range(2, 6)]
    public int numberOfPlayers;

    public Player[] players;
    public int rootables;

    public TurnBasedSystem tbs;

    public GameObject assistant;





    // Start is called before the first frame update

    void Start()
    {
        if (sizeOfMap > 3)
        {
            CreateMap(sizeOfMap);
            AllocatePlayers();
            Debug.Log(sizeOfMap);
        }
        else if (sizeOfMap==3)
        {
            CreateTutorialMap();
            AllocatePlayers();
            assistant.SetActive(true);
            Debug.Log(sizeOfMap);
        }
    }

    // Update is called once per frame
  
    public void CreateTutorialMap()
    {
        int id = 0;
        Debug.Log("Map Creation Started");
        for (int i = -3 + 1; i < 3; i++)
        {
            x = (float)i / 3 * scale;

            for (int j = -3 + 1; j < (3 - Mathf.Abs(i)); j++)
            {

                z = ((float)j + Mathf.Abs((float)i) / 2) * Mathf.Sqrt(3.0f) / 4 * scale;
                fieldPosition = new Vector3(x, y, z);
                // Grass to the center star
                if (i == 0)
                {
                    InstatiateField(grassTilePrefab, id, i, j);
                }
                else if (j == 0)
                {
                    InstatiateField(grassTilePrefab, id, i, j);
                }
                else if (i == j)
                {
                    InstatiateField(grassTilePrefab, id, i, j);
                }
                else if (i == -j)
                {
                    InstatiateField(grassTilePrefab, id, i, j);
                }
                else
                {
                    TileTypesEnum rtt = RandomTileType();
                    if (rtt == TileTypesEnum.Rock)
                    {
                        InstatiateField(rockTilePrefab, id, i, j);

                    }
                    else if (rtt == TileTypesEnum.Sand)
                    {
                        InstatiateField(sandTilePrefab, id, i, j);
                    }
                    else if (rtt == TileTypesEnum.Grass)
                    {
                        InstatiateField(grassTilePrefab, id, i, j);
                    }
                    else if (rtt == TileTypesEnum.Water)
                    {
                        InstatiateField(waterTilePrefab, id, i, j);
                    }

                }
                id++;
            }
        }

        Debug.Log("Map Creation Finished");
        foreach (int i in posAndIds.Values)
        {
            GameObject.Find(i.ToString()).GetComponent<TileScript>().AddNeighbours();

        }
        Debug.Log("Neighbours Added");
        rootables = CountRootables();
        Debug.Log($"Rootables Counted: {rootables}");



    }
    /// <summary>
    /// Creating map with given size
    /// </summary>
    /// <param name="size"></param>
    public void CreateMap(int size)
    {
        int id = 0;
        Debug.Log("Map Creation Started");
        for (int i = -size + 1; i < size; i++)
        {
            x = (float)i / 3 * scale;

            for (int j = -size + 1; j < (size - Mathf.Abs(i)); j++)
            {

                z = ((float)j + Mathf.Abs((float)i) / 2) * Mathf.Sqrt(3.0f) / 4 * scale;
                fieldPosition = new Vector3(x, y, z);
                // Grass to the center star
                if (i == 0)
                {
                    InstatiateField(grassTilePrefab, id, i, j);
                }
                else if (j == 0)
                {
                    InstatiateField(grassTilePrefab, id, i, j);
                }
                else if (i == j)
                {
                    InstatiateField(grassTilePrefab, id, i, j);
                }
                else if (i == -j)
                {
                    InstatiateField(grassTilePrefab, id, i, j);
                }
                // Everything else random
                else
                {
                    TileTypesEnum rtt = RandomTileType();
                    if (rtt == TileTypesEnum.Rock)
                    {
                        InstatiateField(rockTilePrefab, id, i, j);

                    }
                    else if (rtt == TileTypesEnum.Sand)
                    {
                        InstatiateField(sandTilePrefab, id, i, j);
                    }
                    else if (rtt == TileTypesEnum.Grass)
                    {
                        InstatiateField(grassTilePrefab, id, i, j);
                    }
                    else if (rtt == TileTypesEnum.Water)
                    {
                        InstatiateField(waterTilePrefab, id, i, j);
                    }

                }
                id++;
            }
        }

        Debug.Log("Map Creation Finished");
        foreach (int i in posAndIds.Values)
        {
            GameObject.Find(i.ToString()).GetComponent<TileScript>().AddNeighbours();

        }
        Debug.Log("Neighbours Added");
        rootables = CountRootables();
        Debug.Log($"Rootables Counted: {rootables}");



    }
    /// <summary>
    /// Function returns random tile type
    /// </summary>
    /// <returns>TileTypesEnum</returns>
    public TileTypesEnum RandomTileType()
    {
        numberOfTypesOfTiles = System.Enum.GetValues(typeof(TileTypesEnum)).Length;
        int rand = new System.Random().Next(0, numberOfTypesOfTiles);
        return (TileTypesEnum)rand;

    }
    /// <summary>
    /// Function instantiate field using given parameters, where i and j are normalized coordinates in asymetric hex grid
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="id"></param>
    /// <param name="i"></param>
    /// <param name="j"></param>
    public void InstatiateField(GameObject prefab, int id, int i, int j)
    {
        GameObject mapTile = Instantiate(prefab, fieldPosition, fieldRotation, tileParent.transform);
        mapTile.name = id.ToString();
        TileScript tileScript = mapTile.GetComponent<TileScript>();
        tileScript.id = id;
        tileScript.coordinates = new Vector2(x, z);
        Vector2 key = new Vector2(i, j);
        tileScript.ijCoordinates = key;
        tileScript.rootable = tileScript.tileType.rootable;
        tileScript.passable = tileScript.tileType.passable;

        tileScript.mapManager = this;

        posAndIds.Add(key, id);
        tiles.Add(id, tileScript);

    }
    /// <summary>
    /// Allocating number of players
    /// </summary>
    /// <param name="numPlayers"></param>
    public void AllocatePlayers()
    {

        foreach (Player p in players)
        {
            if (p.picked == true)
            {
                
                GameObject player = Instantiate(p.Prefab, p.startPos, p.startRot);
                player.name = p.name;
                player.GetComponent<PlayerMovement>().seqId = p.sequence;
                player.GetComponent<MeshRenderer>().material = p.material;
                tbs.players.Add(player);
                

            }
        }
        tbs.Prepare();
    }
    /// <summary>
    /// Clearing information about fields where current player stands, except given one
    /// </summary>
    /// <param name="exceptThisId"></param>
    public void ClearStanders(int exceptThisId)
    {
        foreach (int id in posAndIds.Values)
        {
            if (id != exceptThisId)
            {
                GameObject.Find(id.ToString()).GetComponent<TileScript>().stander = 0;
            }
        }
    }
    /// <summary>
    /// Counting rootables fields on map
    /// </summary>
    /// <returns></returns>
    public int CountRootables()
    {
        int temp = 0;
        foreach (TileScript t in tiles.Values)
        {
            if (t.rootable == true)
            {
                temp++;
            }
        }
        return temp;
    }
}
