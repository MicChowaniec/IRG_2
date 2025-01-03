using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class PlayerScript : MonoBehaviour
{
    public Player player;
    public Animator animator;
    
    private int tileId;
    public static event Action<int> UpdatePlayerPosition;

    

    // Start is called before the first frame update
    public void Start()
    {
        
        transform.position = player.StartPos;
        this.transform.rotation = player.StartRot;
        CheckPosition();
        animator = GetComponent<Animator>();
        animator.SetTrigger("Idle");
        if (player.human==true)
        {
            StrategyCameraControl scc = FindAnyObjectByType<StrategyCameraControl>();
            scc.objectToCenterOn = transform;
        }
    }

    public void CheckPosition()
    {
        SearchForTileWithRaycast();
    }

    public void SearchForTileWithRaycast()
    {
        Vector3 playerPosition = this.transform.position;

        if (Physics.Raycast(playerPosition, Vector3.down, out RaycastHit hit))
        {
            
            if (hit.collider.TryGetComponent<TileScript>(out var tile))
            {
                tile.TSO.stander = player;
                tileId = tile.TSO.id;
                Vector3 tempScale = transform.localScale;
                transform.parent = tile.transform;
                transform.localScale = tempScale;
            }
        }
    }
   
    public void UpdatePosition()
    {
        player.Pos = transform.position;
        player.Rot = transform.rotation;
    }





}
