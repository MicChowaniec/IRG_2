
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using System;

public class TurnBasedSystem : MonoBehaviour
{
    public List<Turn> turns; // List of turns, serialized for Unity Editor
    public Turn ActiveTurn; // Property to access the active turn
    public int activeTurnIndex; //Value between 0 and Length of turns
    public int numberOfTurns;
 
    public GameSettings gameSettings;
 
    private int totalTurns=-1;

    public Image image;

    public int diseaseLevel;

    public static event Action<Turn> CurrentTurnBroadcast;


   
    private void OnEnable()
    {
        PlayerManager.ChangePhase += SetNextTurn;
    }
    private void OnDisable()
    {
        PlayerManager.ChangePhase -= SetNextTurn;
    }

    private void SetNextTurn()
    {
        totalTurns++;
        
        activeTurnIndex = (totalTurns) % numberOfTurns;
        
        ActiveTurn = turns[activeTurnIndex];
        
        image.sprite = ActiveTurn.icon;
       
        CurrentTurnBroadcast?.Invoke(ActiveTurn);

    }







}
