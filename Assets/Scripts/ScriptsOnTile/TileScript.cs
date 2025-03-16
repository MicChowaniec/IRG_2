
using System;
using UnityEngine;
using UnityEngine.Rendering.RenderGraphModule;


public class TileScript : MonoBehaviour
{
    public TileType TT;
    public TileScriptableObject TSO;
    // Update is called once per frame

    public Player activePlayer;
    public TurnBasedSystem tbs;

    public MapManager mapManager;

    public static event Action<Player> AIMoveyoursefl;

    public int SendID(Vector3 vector3)
    {

        return TSO.id;
    }

   
    public void OnEnable()
    {
        MapManager.MapGenerated += AddNeighbours;
        PlayerManager.ActivePlayerBroadcast += ActivePlayerUpdate;
 
    }
    public  void OnDisable()
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
        Debug.Log("Trying Add A Neighbour");
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
                    if (neighbour != null)
                    {
                        if (!TSO.neighbours.Contains(neighbour))
                        {
                            Debug.Log("Neighbour Added");
                            TSO.neighbours.Add(neighbour);
                            
                        }
                    }
                }

            }
        }
        if (TSO.childType == GameObjectTypeEnum.Tree)
        {
            foreach (var n in TSO.neighbours)
            {
                n.rootable = false;
            }
        }
        mapManager.NeighboursAdded();

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

        if (mapManager.posAdnIds.TryGetValue(vectorTemp, out int tempId))
        {
            TileScriptableObject temp;
            if (mapManager.tiles[tempId] != null)
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
    public void CheckForPlayer()
    {

    }
   
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {     
                Debug.Log("Player Entered This Field" + TSO.id);
            TSO.SetStander(other.GetComponent<PlayerScript>().player);
            
            other.GetComponent<PlayerScript>().tile = TSO;
            
            //other.GetComponent<VisionSystem>().ScanForVisible(activePlayer, other.transform.position, activePlayer.eyes);

        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Exit f This Field" + TSO.id);
            TSO.SetStander(null);
            

        }
    }
   
   
}



