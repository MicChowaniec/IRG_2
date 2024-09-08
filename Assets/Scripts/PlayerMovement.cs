using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public Vector3 position3;
    public int seqId;
    public int tileIdLocation;
    public int tileIdDestination;
    public MapManager mapManager;
    public Vector2 destination;
    public bool doYouWantToMove = false;
    public float range = 1.0f;
    public float moveSpeed = 5f;
    public int percent;

    public int bioMass;
    public int energy;
   
    public float energyPercent;
    public TextMeshProUGUI energyUpdateText;

    public int water;
    public float waterPercent;
    public bool rooted;
    public TextMeshProUGUI waterUpdateText;



    // Start is called before the first frame update
    public void Start()
    {
        mapManager = FindAnyObjectByType<MapManager>();
        bioMass = 100;
        position3 = this.transform.position;
        CheckPosition();
        tileIdDestination = tileIdLocation;
        energyUpdateText = GameObject.Find("EnergyCounter").GetComponent<TextMeshProUGUI>();
        if (energyUpdateText != null)
        {
            UpdateEnergy(50);
        }
        else
        {
            Debug.Log("Didn't find EnergyCounter Object");
        }
        waterUpdateText = GameObject.Find("WaterCounter").GetComponent<TextMeshProUGUI>();
        if (energyUpdateText != null)
        {
            UpdateWater(50);
        }
        else
        {
            Debug.Log("Didn't find WaterCounter Object");
        }
        
       


    }

    // Update is called once per frame
    void Update()
    {
        if (doYouWantToMove == true)
        {
            Move(destination);
        }

    }
    /// <summary>
    /// Checking position
    /// </summary>
    public void CheckPosition()
    {
        position3 = this.transform.position;
        SearchForTileWithRaycast();
    }
    /// <summary>
    /// Checking for tile ID (raycast Down)
    /// </summary>
    public void SearchForTileWithRaycast()
    {
        RaycastHit hit;
        Vector3 playerPosition = this.transform.position;

        // Cast a ray downward from the player’s position
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
    /// <summary>
    /// Moving to selected tile
    /// </summary>"
    /// <param name="destination"></param>

    public void Move(Vector2 destination)
    {
        Vector3 destination3 = new Vector3(destination.x, position3.y, destination.y);
        float distance = Vector3.Distance(transform.position, destination3);

        if (distance > 0.01f) // Check if not at the destination
        {
            // Move towards the destination using MoveTowards
            transform.position = Vector3.MoveTowards(transform.position, destination3, moveSpeed * Time.deltaTime);
        }
        else
        {
            doYouWantToMove = false; // Stop moving when close enough
            CheckPosition(); // Check new position after stopping
        }
    }
    /// <summary>
    /// Checks if character can do action with given amount of energy
    /// </summary>
    /// <param name="actionEnergy"></param>
    /// <returns></returns>
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
    /// <summary>
    /// Give + to add energy point, or - to remove
    /// </summary>
    /// <param name="energy"></param>
    public void UpdateEnergy(int actionEnergy)
    {

        energy += actionEnergy;
        if (energy > bioMass)
        {
            energy = bioMass;
        }
        energyPercent = (float)energy / (float)bioMass * 100;
        energyUpdateText.text = energyPercent + "%";

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
        waterPercent = (float)water / (float)bioMass * 100;
        waterUpdateText.text = waterPercent + "%";

    }
  

    

}

