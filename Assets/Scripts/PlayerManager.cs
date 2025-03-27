using UnityEngine;
using System;
using Unity.VisualScripting;
using UnityEditor;

public class PlayerManager : MonoBehaviour
{
    public bool SpectatorMode;
    public Player[] players;
    public GameObject ActionBar;

    public Player activePlayer;
    private int activePlayerIndex=-1;

    public GameObject[] playerInstances;
    public Transform[] playerTransforms;

    public MapManager mapManager;

    public static event Action<Player> ActivePlayerBroadcast;
    public static event Action ChangePhase;
    public static event Action PlayersInstantiated;



    public void OnEnable()
    {   
        MapManager.MapGenerated += AllocatePlayers;
        EndTurn.EndTurnEvent += ChangePlayer;
        MapManager.CountedRootables += UpdatePoints;
    }

    public void OnDisable()
    {
        MapManager.MapGenerated -= AllocatePlayers;

        EndTurn.EndTurnEvent -= ChangePlayer;

        MapManager.CountedRootables -= UpdatePoints;
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
        playerTransforms = new Transform[players.Length];
        int i = 0;
        bool isThereAHuman = false;
        foreach (Player p in players)
        {

            p.Reset();

            GameObject player = Instantiate(p.Prefab, p.Pos, p.Rot);
            player.name = p.name;
            if (p.human)
            {
                isThereAHuman = true;
                FindAnyObjectByType<StrategyCameraControl>().objectToCenterOn = player.transform;
            }
          
            playerInstances[i] = player; // Assign instantiated player
            playerTransforms[i] = player.GetComponent<Transform>();
            i++;
        }
        if(!isThereAHuman)
        {
            FindAnyObjectByType<StrategyCameraControl>().objectToCenterOn = mapManager.originalTree.transform;
        }
        
        MapManager.MapGenerated -= AllocatePlayers;
        PlayersInstantiated?.Invoke();

        ChangePlayer();
 
    }
    public Transform GetTransformFromSO(Player player)
    {
        return playerTransforms[player.id];
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
        BroadcastPlayer();
        ActionBar.SetActive(activePlayer.human);
        

    }
    private void BroadcastPlayer()
    {
        ActivePlayerBroadcast?.Invoke(activePlayer);
    }
    private void UpdatePoints(int[] points,int id)
    {
        for (int i = 0; i < points.Length; i++)
        {
            players[i].ownedFields = points[i];
        }

    }


}
