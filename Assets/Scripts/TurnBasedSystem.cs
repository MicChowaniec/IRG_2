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
    public  Turn ActiveTurn; // Property to access the active turn
    public int activeTurnIndex; //Value between 0 and Length of turns
    public int numberOfTurns;
 
    public GameSettings gameSettings;
 
    public int totalTurns;

    public Image image;

    public int diseaseLevel;


    void Start()
    {
        numberOfTurns = 6;
        totalTurns = 0;
        activeTurnIndex = 0;
        ActiveTurn = turns[activeTurnIndex];
        image.sprite = ActiveTurn.icon;
    }
    public void OnEnable()
    {
        PlayerManager.ChangePhase += SetNextTurn;

    }
    public void OnDisable()
    {
       PlayerManager.ChangePhase -= SetNextTurn;

    }

    public void SetNextTurn()
    {
        totalTurns++;
        Debug.Log("Step 1");
        activeTurnIndex = (totalTurns) % numberOfTurns;
        Debug.Log("Step 2");
        ActiveTurn = turns[activeTurnIndex];
        Debug.Log("Step 3");
        image.sprite = ActiveTurn.icon;
        Debug.Log("Step 4");

    }







}
