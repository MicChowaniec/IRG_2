using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public Vector3 position;
    public int seqId;
    public int tileIdLocation;
    public int tileIdDestination;
    public MapManager mapManager;
    // Start is called before the first frame update
    void Start()
    {
        mapManager = FindObjectOfType<MapManager>();
        position = this.transform.position;
        CheckPosition();
    }

    // Update is called once per frame
    void Update()
    {
        CheckPosition();
    }

    public void CheckPosition()
    {
        position = this.transform.position;
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
    /// Transform Player from one field to another
    /// </summary>
    /// <param name="tileIdLocation"></param>
    /// <param name="tileIdDestination"></param>
    public void Move(Vector2 destination)
    {
        
    }

}

