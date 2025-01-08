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
    private Turn ActiveTurn; // Property to access the active turn
    public int activeTurnIndex; //Value between 0 and Length of turns
    private int numberOfTurns;
 
    public GameSettings gameSettings;
 
    public int totalTurns;

    public Image image;

    public int diseaseLevel;

    public static event Action NextTurn;

    void Start()
    {
        totalTurns = 0;
        activeTurnIndex = 0;
        ActiveTurn = turns[activeTurnIndex];
        image.sprite = ActiveTurn.icon;
    }


    public void SetNextTurn()
    {
        numberOfTurns = turns.Count;
        activeTurnIndex = (activeTurnIndex + 1) % numberOfTurns;
        ActiveTurn = turns[activeTurnIndex];
        image.sprite = ActiveTurn.icon;
        NextTurn?.Invoke();
    }

    public void OnEnable()
    {
        
    }



}
