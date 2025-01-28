using UnityEngine;
using System;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEditor;

public class PlayerManager : MonoBehaviour
{
    public Player[] players;
    public GameObject ActionBar;

    public Player activePlayer;
    private int activePlayerIndex=-1;

    public GameObject[] playerInstances;


    public static event Action<int> ActivePlayerBroadcast;
    public static event Action ChangePhase;
    public static event Action PlayersInstantiated;

    public void OnEnable()
    {
        EndTurn.EndTurnEvent += ChangePlayer;
        PlayerScript.FinishTurn += ChangePlayer;
        MapManager.MapGenerated += AllocatePlayers;
      

    }

    public void OnDisable()
    {
        EndTurn.EndTurnEvent -=  ChangePlayer;
        PlayerScript.FinishTurn -= ChangePlayer;
        MapManager.MapGenerated -= AllocatePlayers;
        
    }
    /// <summary>
    /// Allocate player only once, after map is created
    /// </summary>
    /// 

    public void AllocatePlayers()
    {
        if (players == null || players.Length == 0)
        {
            Debug.LogError("Player list is empty. Please assign players.");
            return;
        }

        // Initialize the array with the correct size
        playerInstances = new GameObject[players.Length];

        int i = 0;
        foreach (Player p in players)
        {
            p.Reset();

            GameObject player = Instantiate(p.Prefab, p.Pos, p.Rot);
            player.name = p.name;
            if (p.human)
            {
                FindAnyObjectByType<StrategyCameraControl>().objectToCenterOn = player.transform;
            }
            playerInstances[i] = player; // Assign instantiated player
            i++;
        }

        // Unsubscribe from MapGenerated event to prevent multiple allocations
        MapManager.MapGenerated -= AllocatePlayers;

        // Change to the first active player
        ChangePlayer();

        // Notify that players have been instantiated
        PlayersInstantiated?.Invoke();
    }
    public Transform GetTransformFromSO(Player player)
    {
        return playerInstances[player.id].GetComponent<Transform>();
    }
    public GameObject GetGameObjectFromSO(Player player)
    {
        return playerInstances[player.id];
    }

    public void ChangePlayer()
    {
        if (players == null || players.Length == 0)
        {
            Debug.LogError("Player list is empty. Unable to change player.");
            return;
        }

        activePlayerIndex = (activePlayerIndex + 1) % players.Length;
        if (activePlayerIndex%players.Length==0)
        {

            ChangePhase?.Invoke(); Debug.Log("Invoked Changed Phase");
        }


        activePlayer = players[activePlayerIndex];
        Debug.Log(activePlayer.itsName);

        // Safely invoke the event
        ActivePlayerBroadcast?.Invoke(activePlayer.id);
        ActionBar.SetActive(activePlayer.human);
        

    }



}
