using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class PlayerScript : MonoBehaviour
{
    public Player player;

    public List<GameObject> visibleTiles;
    private int tileId;
    public static event Action<string> UpdatePlayerPosition;

    // Start is called before the first frame update
    public void Start()
    {
        this.transform.position = player.Pos;
        CheckPosition();
    }

    public void CheckPosition()
    {
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
                tile.stander = player.itsName;
            }
        }
    }
    public void OnEnable()
    {
      
    }
    //public 

   




}
