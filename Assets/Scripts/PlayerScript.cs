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

    public int bioMass;
    public int energy;
    public int water;
    public bool rooted;

    public int starlings;
    public int maxStarlings;


    public bool isThereABird = false;



    public List<GameObject> visibleTiles;




    // Start is called before the first frame update
    public void Start()
    { 
        position3 = this.transform.position;
        CheckPosition();
        tileIdDestination = tileIdLocation;
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
            }
        }
    }

   




}
