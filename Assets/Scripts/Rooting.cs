using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Rooting : MonoBehaviour
{
    [SerializeField]
    GameObject treePrefab;
    MapManager mapManager;
    ScoreKeeper scoreKeeper;

    // Start is called before the first frame update
    void Start()
    {
        scoreKeeper=GameObject.FindAnyObjectByType<ScoreKeeper>();
        mapManager=GameObject.FindAnyObjectByType<MapManager>();    
        Root(Vector2.zero);
    }

    private void Update()
    {

    }

    public void Root()
    {

        //TODO apply for all players
        PlayerMovement playerMovement = GameObject.Find("RedPlayer").GetComponent<PlayerMovement>();
        TileScript tileScript = GameObject.Find(playerMovement.tileIdLocation.ToString()).GetComponent<TileScript>();
        if (tileScript.rootable == true)
        {
            Instantiate(treePrefab, playerMovement.position3, Quaternion.identity);
            tileScript.rootable = false;
            tileScript.passable = false;
            tileScript.owner = playerMovement.seqId;
            foreach (int NeighbourId in tileScript.neighbours)
            {
                tileScript = GameObject.Find(NeighbourId.ToString()).GetComponent<TileScript>();
                tileScript.rootable = false;
                if (tileScript.owner == 0)
                {
                    tileScript.owner = playerMovement.seqId;


                }

            }
            scoreKeeper.UpdateScore();
        }
    }
    public void Root(Vector2 start)
    {
        // Check if mapManager and posAndIds are properly initialized
        if (mapManager == null || mapManager.posAndIds == null)
        {
            Debug.LogError("MapManager or posAndIds is null.");
            return;
        }

        // Check if the start position exists in posAndIds
        if (!mapManager.posAndIds.ContainsKey(start))
        {
            Debug.LogError("Start position not found in posAndIds.");
            return;
        }

        // Find the tile GameObject by its ID
        string tileId = mapManager.posAndIds[start].ToString();
        GameObject tileObject = GameObject.Find(tileId);
        if (tileObject == null)
        {
            Debug.LogError($"No GameObject found with the name: {tileId}");
            return;
        }

        TileScript tileScript = tileObject.GetComponent<TileScript>();
        if (tileScript == null)
        {
            Debug.LogError("TileScript component not found on the GameObject.");
            return;
        }

        // Debugging: Check if the tile is rootable and its initial state
        Debug.Log($"Checking tile - ID: {tileScript.id}, rootable: {tileScript.rootable}, coordinates: {tileScript.coordinates}");

        // Apply rooting logic if the tile is rootable
        if (tileScript.rootable)
        {
            // Instantiate the first tree and scale it up
            GameObject firstTree = Instantiate(treePrefab, tileScript.transform.position, Quaternion.identity);
            firstTree.transform.localScale = new Vector3(2, 2, 2);

            if (firstTree == null)
            {
                Debug.LogError("Tree object instantiation failed.");
                return;
            }

            // Set properties on the initial tile
            tileScript.rootable = false;
            tileScript.passable = false;
            tileScript.owner = 0;

            Debug.Log("Rooting at tile - ID: " + tileScript.id + ", Coordinates: " + tileScript.coordinates);

            // Process each neighbor
            foreach (int NeighbourId in tileScript.neighbours)
            {
                GameObject neighbourObject = GameObject.Find(NeighbourId.ToString());
                if (neighbourObject == null)
                {
                    Debug.LogError($"No GameObject found with neighbour ID: {NeighbourId}");
                    continue;
                }

                TileScript neighbourTileScript = neighbourObject.GetComponent<TileScript>();
                if (neighbourTileScript != null)
                {
                    Debug.Log("Processing neighbour - ID: " + neighbourTileScript.id);

                    // Set properties on each neighbor tile
                    neighbourTileScript.rootable = false;

                    // Optional: if you want to apply additional logic to neighbors, do it here
                }
                else
                {
                    Debug.LogError("TileScript component not found on the neighbor GameObject.");
                }
            }
        }
        else
        {
            Debug.Log("Tile is not rootable. ID: " + tileScript.id + ", Coordinates: " + tileScript.coordinates);
        }
    }

}


