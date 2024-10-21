using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool picked;

    public Vector3 position3;
    public int seqId;
    public int tileIdLocation;
    public int tileIdDestination;
    public MapManager mapManager;
    public Vector2 destination;
    public bool doYouWantToMove = false;
    public bool doYouWantToFly = false;
    public float range = 1.0f;
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
    public GameObject starlingPrefab;
    private GameObject starling;
    public bool isThereABird = false;
    

    private Vector3 initialPosition; // To store the initial position of the starling

    // Start is called before the first frame update
    public void Start()
    {
        mapManager = FindAnyObjectByType<MapManager>();
        ss = FindAnyObjectByType<ScoutingSystem>();
        rooted = false;
        maxStarlings=1;
        starlingPrefab = ss.StarlingPrefab;
        bioMass = 100;
        position3 = this.transform.position;
        CheckPosition();
        tileIdDestination = tileIdLocation;
        initialPosition = position3; // Set the initial position
        UpdateEnergy(100);
        UpdateWater(100);
        UpdateStarlings(1);
      
       
    }

    // Update is called once per frame
    void Update()
    {
        if (doYouWantToMove)
        {
            Move(destination);
        }
        if (doYouWantToFly)
        {
            if (!isThereABird)
            {
                MoveStarling(destination);
            }
        }
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

    public void Move(Vector2 destination)
    {
        Vector3 destination3 = new Vector3(destination.x, position3.y, destination.y);
        float distance = Vector3.Distance(transform.position, destination3);

        if (distance > 0.01f)
        {
            Debug.Log("Distance: " + distance);
            transform.position = Vector3.MoveTowards(transform.position, destination3, moveSpeed * Time.deltaTime);
        }
        else
        {
            doYouWantToMove = false;
            CheckPosition();
        }
    }

    public void MoveStarling(Vector2 destination)
    {
        
        // Instantiate starling only once
        if (!isThereABird)
        {
            starling = Instantiate(starlingPrefab, position3, Quaternion.identity);
            isThereABird = true;
            StartCoroutine(MoveStarlingCoroutine(destination));
        }

        
    }

    private IEnumerator MoveStarlingCoroutine(Vector2 destination)
    {
        Vector3 destination3 = new Vector3(destination.x, position3.y + 1.0f, destination.y);

        // Move towards the destination
        while (Vector3.Distance(starling.transform.position, destination3) > 0.01f)
        {
            starling.transform.position = Vector3.MoveTowards(starling.transform.position, destination3, moveSpeed * Time.deltaTime);
            yield return null;
        }

        Debug.Log("Starling Reached the Destination");

        // Wait for 1 second
        yield return new WaitForSeconds(0.5f);

        // Move back to the starting position
        while (Vector3.Distance(starling.transform.position, initialPosition) > 0.01f)
        {
            starling.transform.position = Vector3.MoveTowards(starling.transform.position, initialPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        Debug.Log("Starling Returned to Start Position");
        isThereABird = false; // Reset the flag after returning to the starting point
        doYouWantToFly = false;
        Destroy(starling);
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


        if (picked == true)
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


        if (picked == true)
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
        if (picked == true)
        {
            sk.bioMassUpdateText.text = bioMass.ToString();
        }
    }

    public void UpdateStarlings(int addOrRemove)
    {
        starlings += addOrRemove;
        if (picked == true)
        {
            sk.starlingsCounterText.text = starlings + "/" + maxStarlings;
        }
    }
}
