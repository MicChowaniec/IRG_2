
using UnityEngine;
using System.Collections;
using System;
using UnityEditor;
using System.Collections.Generic;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;


public class AI : MonoBehaviour
{

    public ActionManager actionManager;
    public TurnBasedSystem turnBasedSystem;
    public PlayerManager playerManager;

    private Coroutine movementCoroutine;

    public EndTurn endTurn;
    public Player activePlayer;
    public GameObject activePlayerGameObject;
    public TileScriptableObject tileWherePlayerStands;



    public static event Action CountFields;
    public void OnEnable()
    {
        PlayerManager.ActivePlayerBroadcast += ChangePlayer;
    }

    public void OnDisable()
    {
        PlayerManager.ActivePlayerBroadcast -= ChangePlayer;
    }

    
    public void ChangePlayer(Player player)
    {
        if (player.human)
        {
            return;
        }
        else
        {
            Debug.Log("Jestê Komputerê");
            activePlayer = player;
            activePlayerGameObject = playerManager.GetGameObjectFromSO(activePlayer);
            tileWherePlayerStands = activePlayerGameObject.GetComponent<PlayerScript>().tile;
            StartCoroutine(ChooseAMoveCoroutine());
        }

    }

    public void ChooseAMove()
    {
        
        if (activePlayer.water < 5)
        {
            Debug.Log("Woda");
            foreach (var nei in tileWherePlayerStands.neighbours)
            {
                if (nei.gote == GameObjectTypeEnum.Water)
                {
                    activePlayer.Refill();
                   
                }
            }
        }
        if (tileWherePlayerStands.rootable)
        {
            Debug.Log("Ziemia");
            Vector3 rememberLocalScale = activePlayerGameObject.transform.localScale;
            GameObject tree = Instantiate(activePlayer.TreePrefab, activePlayerGameObject.transform.position, Quaternion.identity, tileWherePlayerStands.representation.transform);
            tree.transform.localScale = rememberLocalScale / 5;

            tileWherePlayerStands.rootable = false;
            tileWherePlayerStands.passable = false;
            tileWherePlayerStands.SetOwner(activePlayer);
            
            foreach (var tile in tileWherePlayerStands.neighbours)
            {
                tile.rootable = false;
                tile.SetOwner(activePlayer);


            }
            CountFields?.Invoke();
            FinishThisTurn();
            return;
        }

        else
        {
            List<TileScriptableObject> tileWherePlayerCouldGo = new();
            foreach (var nei in tileWherePlayerStands.neighbours)
            {
                if (nei.passable)
                {
                    tileWherePlayerCouldGo.Add(nei);
                    Debug.Log("Wiatr");

                }
            }
            System.Random rand = new System.Random();


            MoveTo(tileWherePlayerCouldGo[rand.Next(tileWherePlayerCouldGo.Count)].coordinates);


        }

    }



    public void MoveTo(Vector3 newDestination)
    {
       

        if (movementCoroutine != null)
        {
            StopCoroutine(movementCoroutine);
        }

        movementCoroutine = StartCoroutine(SmoothlyMoveToDestination(newDestination));
    }

    private IEnumerator SmoothlyMoveToDestination(Vector3 destination)
    {

        Transform transformOfPlayer = activePlayerGameObject.transform;
        transformOfPlayer.LookAt(destination, Vector3.up);
        activePlayerGameObject.GetComponent<Animator>().SetTrigger("Move");

        while (Vector3.Distance(transformOfPlayer.position, destination) > 0.1f)
        {
            transformOfPlayer.position = Vector3.MoveTowards(transformOfPlayer.position, destination, Time.deltaTime);
            yield return null;
        }
        activePlayerGameObject.GetComponent<Animator>().SetTrigger("Idle");
        transformOfPlayer.position = destination;
        movementCoroutine = null;
        FinishThisTurn();

    }
    private void FinishThisTurn()
    {
        StartCoroutine(DelayedFinishTurn());
    }

    private IEnumerator DelayedFinishTurn()
    {
        yield return new WaitForSeconds(1f);
        playerManager.ChangePlayer();
    }

    private IEnumerator ChooseAMoveCoroutine()
    {
        yield return new WaitUntil(() => tileWherePlayerStands != null);
        
        ChooseAMove();
    }
}






