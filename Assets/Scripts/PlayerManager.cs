using UnityEngine;
using System;
using Unity.VisualScripting;

public class PlayerManager : MonoBehaviour
{
    public Player[] players;
    [HideInInspector]
    public Player activePlayer;
    private int activePlayerIndex;

    public static event Action<Player> ActivePlayerBroadcast;

    public void Start()
    {
        activePlayerIndex = 0;
        activePlayer = players[activePlayerIndex];
        activePlayer.GetComponent<VisionSystem>().ScanForVisible();
    }
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

        // Safely invoke the event
        ActivePlayerBroadcast?.Invoke(activePlayer);
    }
}
