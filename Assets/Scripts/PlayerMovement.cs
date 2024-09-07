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


    // Start is called before the first frame update
    void Start()
    {
        mapManager = FindAnyObjectByType<MapManager>();
        position3 = this.transform.position;
        CheckPosition();
        tileIdDestination = tileIdLocation;
        
    }

    // Update is called once per frame
    void Update()
    {
       if(doYouWantToMove==true)
        {
            Move(destination);
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
    /// </summary>
    /// <param name="destination"></param>
    public void Move(Vector2 destination)
    {

        Vector3 destination3 = new Vector3(destination.x, position3.y, destination.y);
        Vector3 direction = (destination3 - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, destination3);

        if (distance <= range)
        {
            if (distance > 0.01f) // Only move if not at the destination
            {
                // Move towards the destination with Translate
                transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
            }
            else
            {
                doYouWantToMove = false; // Stop moving when close enough
            }
        }
        CheckPosition(); // Check new position after moving

    }
    


}

