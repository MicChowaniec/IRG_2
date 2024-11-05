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
    public int stander;
    public List<int> neighbours = new List<int>();
    public MapManager mapManager;
    [SerializeField]
    public Material litMaterial;
    PlayerScript ps;
    PlayerMovement pm;
    public int activePlayer;
    public bool rootable= true;
    public bool passable = true;
    public MovementSystem ms;
    public ScoutingSystem ss;
    public TurnBasedSystem tbs;
    public GameObjectTypeEnum gote;
    public bool hasRock = false;

    private void Start()
    {
        ss = FindAnyObjectByType<ScoutingSystem>();
        tbs = FindAnyObjectByType<TurnBasedSystem>();
        ms = FindAnyObjectByType<MovementSystem>();
        mapManager = FindAnyObjectByType<MapManager>();
    }
    void Update()
    {

    }
    public void AddNeighbours()
    {
        
        for (int i = -1;i<2;i++)
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
    }
    public int Neighbour(int i, int j)
    {
        int tempId;
        Vector2 vectorTemp = new Vector2(0,0);
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
    private void OnMouseOver()
    {
        if (IsPointerOverUI())
        {
            OnMouseExit();
            return; // Exit early if over UI
        }
        Material[] materials = new Material[2];
        materials[0] = this.GetComponent<MeshRenderer>().material;
        materials[1] = litMaterial;
        this.GetComponent<MeshRenderer>().materials = materials;
    }
    private void OnMouseExit()
    {

        Material[] materials = new Material[1];
        materials[0] = this.GetComponent<MeshRenderer>().material;
        this.GetComponent<MeshRenderer>().materials = materials;
    }

    private void OnMouseDown()
    {
        if (Vector3.Distance(tbs.activePlayer.transform.position, transform.position) < 1) {

            pm = tbs.activePlayer.GetComponent<PlayerMovement>();
            if (tbs.ActiveTurn.nameOfTurn == "SolarTurn")
            {
                if (ms.movable == true)
                {
                    if (IsPointerOverUI())
                    {
                        OnMouseExit();
                        return; // Exit early if over UI
                    }
                    //TODO add other players to move.
                    if (passable == true)
                    {
                        
                            if (ps.CheckForEnergy(50))
                            {
                                ps.UpdateEnergy(-50);
                                Debug.Log("Click");

                                //Call function "Move" from "Player Movement"
                                pm.GetComponent<PlayerMovement>().Move(transform.position);
                            }
                    }
                }
            }
        }
        else if (tbs.ActiveTurn.nameOfTurn == "ThirdEyeTurn")
        {
            ps = tbs.activePlayer.GetComponent<PlayerScript>();
            if (IsPointerOverUI())
            {
                OnMouseExit();
                return; // Exit early if over UI
            }
            if (ss.canYouFly == true)
            {
                if (gote == GameObjectTypeEnum.Bush)
                {
                    if (ps.starlings >= 1)
                    {
                        if (ps.CheckForEnergy(10))
                        {
                            ps.UpdateEnergy(-10);

                            //TODO: change it
                            //Call function "MoveStarling" from "Player Movement"
                            ps.destination = coordinates;
                            ps.tileIdDestination = id;
                            ps.doYouWantToFly = true;
                            ps.bioMass += 20;
                            ps.UpdateStarlings(-1);
                        }
                    }
                }
            }

        }
    }
    private bool IsPointerOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

}



