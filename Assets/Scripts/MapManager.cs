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

    public GameSettings gameSettings;
    private int sizeOfMap;

    public GameObject tilePrefab;

    public TileType rockTileType;
    public TileType sandTileType;
    public TileType waterTileType;
    public TileType grassTileType;

    public GameObject[] bushPrefabs;

    public GameObject rockPrefab;

    public GameObject tileParent;

    public float x;
    public float y;
    public float z;
    public Vector3 fieldPosition;
    public Quaternion fieldRotation = Quaternion.identity;
    public int numberOfTypesOfTiles;
    public Dictionary<int, TileScriptableObject> tiles = new();
    public Dictionary<Vector2, int> posAdnIds = new();
    public Dictionary<int, Color> colors = new();
    public GameObject originalTreePrefab;

    [Range(1, 10)]
    public int scale;


    public TurnBasedSystem tbs;
    
    public GameObject assistant;
    public static int centerId;
    public static event Action MapGenerated;
    public static event Action<int> Rootables;

    // Start is called before the first frame update

    void Start()
    {

        colors[0] = Color.magenta; colors[1] = Color.blue; colors[2] = Color.green; colors[3] = Color.yellow; colors[4] = Color.gray; colors[5] = Color.red ;
        sizeOfMap = gameSettings.SizeOfMap;
        if (sizeOfMap > 3)
        {
            CreateMap(sizeOfMap);

            Debug.Log(sizeOfMap);
        }
        else if (sizeOfMap==3)
        {
            CreateTutorialMap();

            assistant.SetActive(true);
            Debug.Log(sizeOfMap);
            
        }
        MapGenerated?.Invoke();
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
                    
                    InstatiateField(grassTileType, id, i, j);
                }
                else if (j == 0)
                {
                    InstatiateField(grassTileType, id, i, j);
                }
                else if (i == j)
                {
                    InstatiateField(grassTileType, id, i, j);
                }
                else if (i == -j)
                {
                    InstatiateField(grassTileType, id, i, j);
                }
                else if (i == 2 && j == -1)
                {
                    InstatiateField(waterTileType, id, i, j);
                }
                else if (i == 1 && j == -2)
                {
                    InstatiateField(grassTileType, id,i,j);
                    InstantiateBush(bushPrefabs[2], id);
                    tiles[id].childType=GameObjectTypeEnum.Bush;
                    
                }
                else
                {
                    TileTypesEnum rtt = RandomTileType();
                    if (rtt == TileTypesEnum.Rock)
                    {
                        InstatiateField(rockTileType, id, i, j);

                    }
                    else if (rtt == TileTypesEnum.Sand)
                    {
                        InstatiateField(sandTileType, id, i, j);
                    }
                    else if (rtt == TileTypesEnum.Grass)
                    {
                        InstatiateField(grassTileType, id, i, j);
                    }
                    else if (rtt == TileTypesEnum.Water)
                    {
                        InstatiateField(waterTileType, id, i, j);
                    }

                }
                id++;
            }
        }

        
        



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
                    InstatiateField(grassTileType, id, i, j);
                    if (j==0)
                    {
                        GameObject tree = Instantiate(originalTreePrefab, tiles[id].representation.transform);
                        tree.transform.localScale =new Vector3(0.2f, 20, 0.2f);
                        centerId = id;
                    }
                    
                }
                else if (j == 0)
                {
                    InstatiateField(grassTileType, id, i, j);
                }
                else if (i == j)
                {
                    InstatiateField(grassTileType, id, i, j);
                }
                else if (i == -j)
                {
                    InstatiateField(grassTileType, id, i, j);
                }
                // Everything else random
                else
                {
                    TileTypesEnum rtt = RandomTileType();
                   
                    if (rtt == TileTypesEnum.Rock)
                    {
                        InstatiateField(rockTileType, id, i, j);
                        if (2 > new System.Random().Next(0, 4))
                        {
                            //int random = new System.Random().Next(0, bushPrefabs.Length);
                            GameObject rock = Instantiate(rockPrefab, tiles[id].coordinates, Quaternion.identity, tiles[id].representation.transform);
                            rock.transform.localScale = new Vector3(1, 40, 1);
                            
                            tiles[id].childType =GameObjectTypeEnum.Rock;
                    

                        }
                    }
                    else if (rtt == TileTypesEnum.Sand)
                    {
                        InstatiateField(sandTileType, id, i, j);
                    }
                    else if (rtt == TileTypesEnum.Grass)
                    {
                        InstatiateField(grassTileType, id, i, j);
                        if (2 >= new System.Random().Next(0,4))
                        {
                            int random = new System.Random().Next(0, bushPrefabs.Length);
                            InstantiateBush(bushPrefabs[random], id);
                            // 0 - purple, 1 - blue, 2 - green, 3 - yellow, 4 - orange, 5 - red; Use dictionary "colors";
                            
                            tiles[id].childColor = colors[random];
                            tiles[id].childType = GameObjectTypeEnum.Bush;
                            tiles[id].passable = false;

                        }
                    }
                    else if (rtt == TileTypesEnum.Water)
                    {
                        InstatiateField(waterTileType, id, i, j);
                        tiles[id].childType = GameObjectTypeEnum.Water;
                    }

                }
                id++;
            }
        }



        Debug.Log("Map Creation Finished");
        MapGenerated?.Invoke();
        int temp = CountRootables();
        Rootables?.Invoke(temp);




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
    public void InstatiateField(TileType tt, int id, int i, int j)
    {
        TileScriptableObject TSO = ScriptableObject.CreateInstance<TileScriptableObject>();
        
        TSO.id = id;
        TSO.coordinates = new Vector3(x, 0, z);
        Vector2 key = new(i, j);
        TSO.ijCoordinates = key;
        TSO.tileTypes = tt.type;
        TSO.passable = tt.passable;
        TSO.rootable = tt.rootable;
        TSO.representation = Instantiate(tilePrefab, fieldPosition, fieldRotation, tileParent.transform);
        TSO.representation.name = id.ToString();
        TSO.representation.GetComponent<MeshRenderer>().material = tt.Material;
        TileScript tileScript = TSO.representation.GetComponent<TileScript>();
        tileScript.TSO = TSO;
        tileScript.TT = tt;
        tileScript.mapManager = this;

        OnHoverScript onHoverScript = TSO.representation.GetComponent<OnHoverScript>();


        onHoverScript.onHoverSC = TSO;


        tiles.Add(id, TSO);
        posAdnIds.Add(key, id);

        
        
        
        


    }
    public void InstantiateBush(GameObject prefab, int id)
    {
        GameObject bushObject = Instantiate(prefab, fieldPosition, fieldRotation, tiles[id].representation.transform);
        bushObject.transform.localScale = new Vector3(0.5f, 20, 0.5f);
        bushObject.transform.position += new Vector3(0, 0.2f, 0);


    }

   
    /// <summary>
    /// Clearing information about fields where current player stands, except given one
    /// </summary>
    /// <param name="exceptThisId"></param>
    public void ClearStanders(int exceptThisId)
    {
       //Do zrobienia
    }
    /// <summary>
    /// Counting rootables fields on map
    /// </summary>
    /// <returns></returns>
    public int CountRootables()
    {
        int temp = 0;
        foreach (TileScriptableObject t in tiles.Values)
        {
            if (t.rootable == true)
            {
                temp++;
            }
        }
        return temp;
    }
   
}
