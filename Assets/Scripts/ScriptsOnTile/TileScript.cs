using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.RenderGraphModule;

public class TileScript : MonoBehaviour
{
    public TileType TT;
    public TileScriptableObject TSO;
    // Update is called once per frame

    public Player activePlayer;
    public TurnBasedSystem tbs;

    public MapManager mapManager;

   
    public int SendID(Vector3 vector3)
    {

        return TSO.id;
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
                    TileScriptableObject neighbour = Neighbour(i, j);
                    if (neighbour!=null)
                    {
                        if (!TSO.neighbours.Contains(neighbour))
                        {
                            TSO.neighbours.Add(neighbour);
                            //Debug.Log("Neighbour Added");
                        }
                    }
                }

            }
        }
        if(TSO.childType==GameObjectTypeEnum.Tree)
        {
            foreach (var n in TSO.neighbours)
            {
                n.rootable = false;
            }
        }
        
    }
    public TileScriptableObject Neighbour(int i, int j)
    {
        Vector2 vectorTemp = new(0, 0);
        if (mapManager == null)
        {

            return null;
        }

        if (mapManager.posAdnIds == null)
        {

            return null;
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
            TileScriptableObject temp;
            if (mapManager.tiles[tempId]!=null)
            {
                temp = mapManager.tiles[tempId];
                return temp;
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }
   
}



