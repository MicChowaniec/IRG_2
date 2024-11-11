using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public bool human;
    public int soulLvl;
    public Vector3 position3;
    public int seqId;
    public int tileIdLocation;
    public int tileIdDestination;
    public MapManager mapManager;
    public Vector2 destination;
    public bool doYouWantToMove = false;
    public bool doYouWantToFly = false;
    public float moveSpeed = 5f;
    public int percent;
    public GameObject treePrefab;

    public ScoreKeeper sk;
    public int bioMass;
    public int energy;
    public int water;
    public bool rooted;

    public int starlings;
    public int maxStarlings;
    public ScoutingSystem ss;

    public bool isThereABird = false;

    public List<GameAction> actions;

    public List<GameObject> visibleTiles;




    // Start is called before the first frame update
    public void Start()
    {
        mapManager = FindAnyObjectByType<MapManager>();
        ss = FindAnyObjectByType<ScoutingSystem>();
        rooted = false;
        maxStarlings = 1;
        bioMass = 100;
        position3 = this.transform.position;
        CheckPosition();
        tileIdDestination = tileIdLocation;
        UpdateEnergy(100);
        UpdateWater(100);
        UpdateStarlings(1);



    }

 
   

    public void CheckPosition()
    {
        position3 = this.transform.position;
        SearchForTileWithRaycast();
    }

    public void SearchForTileWithRaycast()
    {
        RaycastHit hit;
        Vector3 playerPosition = this.transform.position;

        if (Physics.Raycast(playerPosition, Vector3.down, out hit))
        {
            TileScript tile = hit.collider.GetComponent<TileScript>();

            if (tile != null)
            {
                tileIdLocation = tile.id;
                tile.stander = seqId;
                mapManager.ClearStanders(tileIdLocation);
            }
        }
    }

   



    public bool CheckForEnergy(int actionEnergy)
    {
        if (actionEnergy <= energy)
        {
            return true;
        }
        else if (actionEnergy > energy)
        {
            return false;
        }
        else
        {
            Debug.Log("unknown problem with this value:" + actionEnergy);
            return false;
        }
    }

    public void UpdateEnergy(int actionEnergy)
    {
        energy += actionEnergy;
        if (energy > bioMass)
        {
            energy = bioMass;
        }


        if (human == true)
        {
            sk.energyUpdateText.text = energy.ToString();
        }
        else
        {
            Debug.LogWarning("energyUpdateText is not assigned.");
        }
    }

    public bool CheckForWater(int actionWater)
    {
        if (actionWater <= water)
        {
            return true;
        }
        else if (actionWater > water)
        {
            return false;
        }
        else
        {
            Debug.Log("unknown problem with this value:" + actionWater);
            return false;
        }
    }

    public void UpdateWater(int actionWater)
    {
        water += actionWater;
        if (water > bioMass)
        {
            water = bioMass;
        }


        if (human == true)
        {
            sk.waterUpdateText.text = water.ToString();
        }
        else
        {
            Debug.LogWarning("waterUpdateText is not assigned.");
        }

    }

    public void UpdateBioMass(int actionBioMass)
    {
        bioMass += actionBioMass;
        if (human == true)
        {
            sk.bioMassUpdateText.text = bioMass.ToString();
        }
    }

    public void UpdateStarlings(int addOrRemove)
    {
        starlings += addOrRemove;
        if (human == true)
        {
            sk.starlingsCounterText.text = starlings + "/" + maxStarlings;
        }
    }

    public void UpdateActions(GameAction action)
    {
        actions.Add(action);
    }

}
