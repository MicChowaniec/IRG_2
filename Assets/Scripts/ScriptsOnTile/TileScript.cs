using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileScript : MonoBehaviour
{
    public TileType TT;
    public TileScriptableObject TSO;
    // Update is called once per frame
    
    public List<int> neighbours = new();
    
    public Player activePlayer;
    public TurnBasedSystem tbs;

    public MapManager mapManager;

    public void Start()
    {
        
    }
    public static int SendID(Vector3 vector3)
    {



        return 0;
            }

   
    private void OnEnable()
    {
        MapManager.MapGenerated += AddNeighbours;
        PlayerManager.ActivePlayerBroadcast += ActivePlayerUpdate;
    }
    private void OnDisable()
    {
        MapManager.MapGenerated -= AddNeighbours;
        PlayerManager.ActivePlayerBroadcast-= ActivePlayerUpdate;
    }

    private void ActivePlayerUpdate(Player player) 
    {
        activePlayer = player;
    }
    private void AddNeighbours()
    {

        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
               
                if (j == 1 & i == -1 & (i + TSO.ijCoordinates.x) < 0) { }
                else if (j == -1 & i == 1 & (i + TSO.ijCoordinates.x) <= 0) { }
                else if (j == 1 & i == 1 & (i + TSO.ijCoordinates.x) > 0) { }
                else if (j == -1 & i == -1 & (i + TSO.ijCoordinates.x) >= 0) { }

                else
                {
                    int neighborId = Neighbour(i, j);
                    if (neighborId != -1)
                    {
                        if (!neighbours.Contains(neighborId))
                        {
                            neighbours.Add(neighborId);
                            Debug.Log("Neighbour Added");
                        }
                    }
                }

            }
        }
        if (TSO.id == MapManager.centerId)
        {
            foreach (var n in neighbours)
            {
                mapManager.tiles[n].rootable = false;

            }
        }
        
    }
    public int Neighbour(int i, int j)
    {
        Vector2 vectorTemp = new(0, 0);
        if (mapManager == null)
        {

            return -1;
        }

        if (mapManager.posAdnIds == null)
        {

            return -1;
        }
        
        if (TSO.ijCoordinates.x < 1)
        {
            vectorTemp = TSO.ijCoordinates + new Vector2(i, j);
        }
        else if (TSO.ijCoordinates.x >= 1)
        {
            vectorTemp = TSO.ijCoordinates + new Vector2(i, j);
        }

        if (mapManager.posAdnIds.TryGetValue(vectorTemp,out int tempId))
        {
            if (tempId != TSO.id)
            {
                return tempId;
            }
            else
            {
                return -1;
            }
        }
        else
        {
            return -1;
        }
    }
   
}



