using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
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

    public int bioMass;
    public TextMeshProUGUI bioMassUpdateText;
    public int energy;
    public TextMeshProUGUI energyUpdateText;

    public int water; 
    public TextMeshProUGUI waterUpdateText;

    public bool rooted;

    public int starlings;
    public int maxStarlings;
    public ScoutingSystem ss;
    public GameObject starlingPrefab;
    private GameObject starling;
    public bool isThereABird = false;
    public TextMeshProUGUI starlingsCounterText;

    private Vector3 initialPosition; // To store the initial position of the starling

    // Start is called before the first frame update
    public void Start()
    {
        maxStarlings=1;
        ss = FindAnyObjectByType<ScoutingSystem>();
        starlingPrefab = ss.StarlingPrefab;
        mapManager = FindAnyObjectByType<MapManager>();
        bioMass = 100;
        position3 = this.transform.position;
        CheckPosition();
        tileIdDestination = tileIdLocation;
        initialPosition = position3; // Set the initial position

        energyUpdateText = GameObject.Find("EnergyCounter").GetComponent<TextMeshProUGUI>();
        waterUpdateText = GameObject.Find("WaterCounter").GetComponent<TextMeshProUGUI>();
        starlingsCounterText = GameObject.Find("StarlingsCounter").GetComponent<TextMeshProUGUI>();

        if (energyUpdateText != null)
        {
            UpdateEnergy(51);
        }
        else
        {
            Debug.Log("Didn't find EnergyCounter Object");
            energyUpdateText = GameObject.Find("EnergyCounter").GetComponent<TextMeshProUGUI>();
        }

        
        if (waterUpdateText != null)
        {
            UpdateWater(52);
        }
        else
        {
            waterUpdateText = GameObject.Find("WaterCounter").GetComponent<TextMeshProUGUI>();
            Debug.Log("Didn't find WaterCounter Object");
        }
       
        if (starlingsCounterText != null)
        {
            UpdateStarlings(1);
        }
        else
        {
            starlingsCounterText = GameObject.Find("StarlingCounter").GetComponent<TextMeshProUGUI>();
            Debug.Log("Didn't find StarlingsCounter Object");
        }
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
        

        if (energyUpdateText != null)
        {
            energyUpdateText.text = energy.ToString();
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


        if (waterUpdateText != null)
        {
            waterUpdateText.text = water.ToString();
        }
        else
        {
            Debug.LogWarning("waterUpdateText is not assigned.");
        }

    }

    public void UpdateBioMass(int actionBioMass)
    {
        bioMass += actionBioMass;
        bioMassUpdateText.text = bioMass.ToString();
    }

    public void UpdateStarlings(int addOrRemove)
    {
        starlings += addOrRemove;
        starlingsCounterText.text = starlings + "/" + maxStarlings;
    }
}
