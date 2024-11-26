using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TurnBasedSystem : MonoBehaviour
{
    public List<Turn> turns; // List of turns, serialized for Unity Editor

    public Turn ActiveTurn; // Property to access the active turn
    public int activeTurnIndex; //Value between 0 and Length of turns
    private int numberOfTurns;

    public GameObject dayUI;

    public GameObject nightUI;
 

    public List<GameObject> players;
    [HideInInspector]
    public GameObject activePlayer;
    [HideInInspector]
    public GameObject pickedPlayer;
    private int numberOfPlayers;
    private int activePlayerIndex;

 
    public MovementSystem ms;


    public int totalTurns;


    public Image image;


    public StrategyCameraControl scc;



    public int diseaseLevel;

    void Start()
    {
        totalTurns = 0;

        activeTurnIndex = 0;
        ActiveTurn = turns[activeTurnIndex];
        image.sprite = ActiveTurn.icon;
    }
    /// <summary>
    /// Start function, called externally
    /// </summary>
    public void Prepare()
    {
        activePlayerIndex = 0;
        activePlayer = players[activePlayerIndex];
        CheckTurn();

        scc.CenterOnObject(pickedPlayer);
        activePlayer.GetComponent<VisionSystem>().ScanForVisible();
    }
    public void ChangePlayer()
    {
        ms.movable = false;
        numberOfPlayers = players.Count;
        if (activePlayerIndex==numberOfPlayers-1)
        {
            SetNextTurn();
        }
        activePlayerIndex = (activePlayerIndex + 1) % numberOfPlayers;
        activePlayer = players[activePlayerIndex];
        totalTurns++;

    }
    public void SetNextTurn()
    {
        numberOfTurns = turns.Count;
        activeTurnIndex = (activeTurnIndex + 1) % numberOfTurns;
        ActiveTurn = turns[activeTurnIndex];
        image.sprite = ActiveTurn.icon;
        // Advance to the next turn and loop back if at the end
    
        CheckTurn();
        // TODO implement buttons managament for turns

    }
   
    /// <summary>
    /// Function Called externally by button, or sequence
    /// </summary>
   
    /// <summary>
    /// Adjust Sun position and UI to the existing Sun position
    /// </summary>
    public void CheckTurn()

    {
      


    }

}
