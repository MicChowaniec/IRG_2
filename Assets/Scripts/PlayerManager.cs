using UnityEngine;
using System;
using Unity.VisualScripting;
using UnityEditor.Animations;

public class PlayerManager : MonoBehaviour
{
    public Player[] players;
    public GameObject ActionBar;

    public Player activePlayer;
    private int activePlayerIndex=-1;


    public static event Action<int> ActivePlayerBroadcast;
    public static event Action ChangePhase;


    public void OnEnable()
    {
        EndTurn.EndTurnEvent += ChangePlayer;
        PlayerScript.FinishTurn += ChangePlayer;
        MapManager.MapGenerated += AllocatePlayers;
        ChangePlayer();
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
    public void AllocatePlayers()
    {
        if (players == null || players.Length == 0)
        {
            Debug.LogError("Player list is empty. Please assign players.");
            return;
        }

        foreach (Player p in players)
        {
            GameObject player = Instantiate(p.Prefab, p.Pos, p.Rot);
            player.name = p.name;
        }
        MapManager.MapGenerated -= AllocatePlayers;
    }

    public void ChangePlayer()
    {
        if (players == null || players.Length == 0)
        {
            Debug.LogError("Player list is empty. Unable to change player.");
            return;
        }

        activePlayerIndex = (activePlayerIndex + 1) % players.Length;

        activePlayer = players[activePlayerIndex];
        Debug.Log(activePlayer.itsName);

        // Safely invoke the event
        ActivePlayerBroadcast?.Invoke(activePlayer.id);
        ActionBar.SetActive(activePlayer.human);
        if (activePlayerIndex%players.Length==0)
        {
            ChangePhase?.Invoke();
        }
    }



}
