using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileScript : MonoBehaviour
{
    [SerializeField]
    public TileType tileType;
    public int id;
    // Update is called once per frame
    public Vector2 coordinates;
    public Vector2 ijCoordinates;
    public int owner;
    public string stander;
    public List<int> neighbours = new List<int>();
    public MapManager mapManager;
    [SerializeField]
    public Material litMaterial;
    public Player activePlayer;
    public TurnBasedSystem tbs;
    public bool hasRock = false;

    public static int SendID(Vector3 vector3) {
        return 0;
            }

    public void Start()
    {
        ScriptableObject.CreateInstance<TileType>();
    }
    private void OnEnable()
    {
        MapManager.MapGenerated += AddNeighbours;
        PlayerManager.ActivePlayerBroadcast += ActivePlayerUpdate;
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
                if (j == 0 & i == 0) { }
                else if (j == 1 & i == -1 & (i + ijCoordinates.x) < 0) { }
                else if (j == -1 & i == 1 & (i + ijCoordinates.x) <= 0) { }
                else if (j == 1 & i == 1 & (i + ijCoordinates.x) > 0) { }
                else if (j == -1 & i == -1 & (i + ijCoordinates.x) >= 0) { }

                else
                {
                    int neighborId = Neighbour(i, j);
                    if (neighborId != -1)
                    {
                        neighbours.Add(neighborId);
                    }
                }

            }
        }
        MapManager.MapGenerated -= AddNeighbours;
    }
    public int Neighbour(int i, int j)
    {
        int tempId;
        Vector2 vectorTemp = new Vector2(0, 0);
        if (mapManager == null)
        {

            return -1;
        }

        if (mapManager.posAndIds == null)
        {

            return -1;
        }
        if (ijCoordinates.x < 1)
        {
            vectorTemp = ijCoordinates + new Vector2(i, j);
        }
        else if (ijCoordinates.x >= 1)
        {
            vectorTemp = ijCoordinates + new Vector2(i, j);
        }


        if (mapManager.posAndIds.TryGetValue(vectorTemp, out tempId))
        {

            return tempId;
        }
        else
        {
            return -1;
        }
    }
    public void Highlight()
    {
        Material[] materials = new Material[2];
        materials[0] = this.GetComponent<MeshRenderer>().material;
        materials[1] = litMaterial;
        this.GetComponent<MeshRenderer>().materials = materials;
    }
    public void StopHighlight()
    {

        Material[] materials = new Material[1];
        materials[0] = this.GetComponent<MeshRenderer>().material;
        this.GetComponent<MeshRenderer>().materials = materials;
    }
    private void OnMouseEnter()
    {
        Highlight();
    }
    private void OnMouseExit()
    {
        StopHighlight();
    }
}



