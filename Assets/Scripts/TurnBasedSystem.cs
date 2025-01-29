using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TurnBasedSystem : MonoBehaviour
{
    public List<Turn> turns; // List of turns, serialized for Unity Editor
    public Turn ActiveTurn; // Property to access the active turn
    private int activeTurnIndex; //Value between 0 and Length of turns
    private int numberOfTurns = 6;
 
    public GameSettings gameSettings;
 
    private int totalTurns=-1;

    public Image image;

    public int diseaseLevel;

    public static event Action<Turn> currentTurnBroadcast;


   
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
        Debug.Log("Turn: "+totalTurns);
        Debug.Log("Step 1");
        activeTurnIndex = (totalTurns) % numberOfTurns;
        Debug.Log("Step 2");
        ActiveTurn = turns[activeTurnIndex];
        Debug.Log("Step 3");
        image.sprite = ActiveTurn.icon;
        Debug.Log("Step 4");
        currentTurnBroadcast?.Invoke(ActiveTurn);

    }







}
