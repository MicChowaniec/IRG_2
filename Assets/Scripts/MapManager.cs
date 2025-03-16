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
    public Dictionary<int, ActionTypeEnum> colors = new();
    public GameObject originalTreePrefab;

    [Range(1, 10)]
    public int scale;


    public TurnBasedSystem tbs;
    
    public GameObject assistant;
    public int centerId;
    public static event Action MapGenerated;

    public static event Action AllNeighboursCounted;

    public int startRootables;
    public static event Action<int[],int> CountedRootables;

    private int neighboursAdded;
    public void NeighboursAdded()
    {
        neighboursAdded++;
        if (neighboursAdded == tiles.Count)
        {
            AllNeighboursCounted?.Invoke();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnEnable()
    {
        PlayerManager.PlayersInstantiated += CountRootables;
        RootSkill.UpdateRootables += CountRootables;
        AI.CountFields += CountRootables;
    }
    /// <summary>
    /// 
    /// </summary>
    public void OnDisable()
    {
        PlayerManager.PlayersInstantiated -= CountRootables;
        RootSkill.UpdateRootables -= CountRootables;
        AI.CountFields -= CountRootables;
    }
    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        startRootables = 0;
        colors[0]= ActionTypeEnum.Purple; colors[1] = ActionTypeEnum.Blue; colors[2] = ActionTypeEnum.Green; colors[3] = ActionTypeEnum.Yellow; colors[4] = ActionTypeEnum.Orange; colors[5] = ActionTypeEnum.Red ;

        sizeOfMap = gameSettings.SizeOfMap;
        if (sizeOfMap > 3)
        {
            CreateMap(sizeOfMap);

            Debug.Log(sizeOfMap);
        }
        else if (sizeOfMap==3)
        {
            //eateTutorialMap();

            assistant.SetActive(true);
            Debug.Log(sizeOfMap);
            
        }
        MapGenerated?.Invoke();
    }

    // Update is called once per frame
  
    //public void CreateTutorialMap()
    //{
    //    int id = 0;
    //    Debug.Log("Map Creation Started");
    //    for (int i = -3 + 1; i < 3; i++)
    //    {
    //        x = (float)i / 3 * scale;

    //        for (int j = -3 + 1; j < (3 - Mathf.Abs(i)); j++)
    //        {

    //            z = ((float)j + Mathf.Abs((float)i) / 2) * Mathf.Sqrt(3.0f) / 4 * scale;
    //            fieldPosition = new Vector3(x, y, z);
    //            // Grass to the center star
    //            if (i == 0)
    //            {
                    
    //                InstatiateField(grassTileType, id, i, j);
    //            }
    //            else if (j == 0)
    //            {
    //                InstatiateField(grassTileType, id, i, j);
    //            }
    //            else if (i == j)
    //            {
    //                InstatiateField(grassTileType, id, i, j);
    //            }
    //            else if (i == -j)
    //            {
    //                InstatiateField(grassTileType, id, i, j);
    //            }
    //            else if (i == 2 && j == -1)
    //            {
    //                InstatiateField(waterTileType, id, i, j);
    //            }
    //            else if (i == 1 && j == -2)
    //            {
    //                InstatiateField(grassTileType, id,i,j);
    //                InstantiateBush(bushPrefabs[2], id);
    //                tiles[id].childType=GameObjectTypeEnum.Bush;
                    
    //            }
    //            else
    //            {
    //                TileTypesEnum rtt = RandomTileType();
    //                if (rtt == TileTypesEnum.Rock)
    //                {
    //                    InstatiateField(rockTileType, id, i, j);

    //                }
    //                else if (rtt == TileTypesEnum.Sand)
    //                {
    //                    InstatiateField(sandTileType, id, i, j);
    //                }
    //                else if (rtt == TileTypesEnum.Grass)
    //                {
    //                    InstatiateField(grassTileType, id, i, j);
    //                }
    //                else if (rtt == TileTypesEnum.Water)
    //                {
    //                    InstatiateField(waterTileType, id, i, j);
    //                }

    //            }
    //            id++;
    //        }
    //    }

        
        



    //}
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
                        Instantiate(originalTreePrefab, tiles[id].representation.transform);

                        centerId = id;
                        tiles[id].childType = GameObjectTypeEnum.Tree;
                        tiles[id].childColor = ActionTypeEnum.None;
                        tiles[id].rootable = false;
                        tiles[id].passable = false;
                    }

                }
                else if (j == 0)
                {
                    InstatiateField(grassTileType, id, i, j);
                    tiles[id].childType = GameObjectTypeEnum.None;
                    tiles[id].childColor = ActionTypeEnum.None;
                }
                else if (i == j)
                {
                    InstatiateField(grassTileType, id, i, j);
                    tiles[id].childType = GameObjectTypeEnum.None;
                    tiles[id].childColor = ActionTypeEnum.None;
                }
                else if (i == -j)
                {
                    InstatiateField(grassTileType, id, i, j);
                    tiles[id].childType = GameObjectTypeEnum.None;
                    tiles[id].childColor = ActionTypeEnum.None;
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
                            Instantiate(rockPrefab, tiles[id].coordinates, Quaternion.identity, tiles[id].representation.transform);


                            tiles[id].childType = GameObjectTypeEnum.Rock;
                            tiles[id].childColor = ActionTypeEnum.None;
                            tiles[id].passable = false;


                        }
                    }
                    else if (rtt == TileTypesEnum.Sand)
                    {
                        InstatiateField(sandTileType, id, i, j);
                        tiles[id].childType = GameObjectTypeEnum.None;
                        tiles[id].childColor = ActionTypeEnum.None;
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
                        tiles[id].childColor = ActionTypeEnum.None;

                    }

                }
                id++;
            }
        }
        



        Debug.Log("Map Creation Finished");
        MapGenerated?.Invoke();





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
        foreach (Transform child in TSO.representation.transform)
        {
            if (child.TryGetComponent<MeshRenderer>(out MeshRenderer component))
            {
                component.material = tt.Material;
            }
        }
        TileScript tileScript = TSO.representation.GetComponent<TileScript>();
        tileScript.TSO = TSO;
        tileScript.TT = tt;
        tileScript.mapManager = this;

        OnHoverScript onHoverScript = TSO.representation.GetComponent<OnHoverScript>();


        onHoverScript.onHoverSC = TSO;


        tiles.Add(id, TSO);
        posAdnIds.Add(key, id);

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="id"></param>
    public void InstantiateBush(GameObject prefab, int id)
    {
        GameObject bushObject = Instantiate(prefab, fieldPosition, fieldRotation, tiles[id].representation.transform);

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
    public void CountRootables()
    {
        tiles[centerId].rootable = false;
        tiles[centerId].passable = false;
        tiles[centerId].gote = GameObjectTypeEnum.Tree;
        tiles[centerId].ate = ActionTypeEnum.None;
        foreach (var tile in tiles[centerId].neighbours)
        {
            tile.rootable = false;
            tile.passable = true;
        }
        int temp = 0;
        int purple = 0;
        int blue = 0;
        int green = 0;
        int yellow = 0;
        int orange = 0;
        int red = 0;

        foreach (TileScriptableObject t in tiles.Values)
        {
           

            if (t.GetOwner() != null)
            {
                switch (t.GetOwner().id)
                {
                    case 0: purple++; break;
                    case 1: blue++; break;
                    case 2: green++; break;
                    case 3: yellow++; break;
                    case 4: red++; break;
                    case 5: orange++; break;
                }
            }

        }
        
        
        int[] fields = { purple, blue, green, yellow, red, orange };
        CountedRootables?.Invoke(fields,startRootables);


    }
   
}
