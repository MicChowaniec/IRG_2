using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TurnBasedSystem : MonoBehaviour
{
    [SerializeField]
    private List<Turn> turns = new List<Turn>(); // List of turns, serialized for Unity Editor
    public Turn ActiveTurn { get; private set; } // Property to access the active turn
    private int activeTurnIndex; //Value between 0 and Length of turns
    private int numberOfTurns;
    private int solarPointer;
    [SerializeField]
    public GameObject dayUI;
    [SerializeField]
    public GameObject nightUI;
    [SerializeField]
    public Light Light;

    void Start()
    {
        //TODO Set Light
        solarPointer = 1;
        numberOfTurns = turns.Count;
        activeTurnIndex = 0;
        

    }

    void Update()
    {
       
        // Add logic to handle turn updates or player inputs here
    }

    public void SetNextTurn()
    {
        // Advance to the next turn and loop back if at the end
        activeTurnIndex = (activeTurnIndex + 1) % numberOfTurns;
        ActiveTurn = turns[activeTurnIndex];
        solarPointer += 5;
        CheckTurn();
        // TODO implement buttons managament for turns
    }
    public void SolarSet()
    {
        dayUI.SetActive(false);
        nightUI.SetActive(true);
        solarPointer = -180;
    }
    public void SolarRise()
    {
        dayUI.SetActive(false);
        nightUI.SetActive(true);
        solarPointer = 1;
    }
    public void CheckTurn()
        
    {
        if (solarPointer % 181 == 0)
        {
            SolarSet();
        }
        else if (numberOfTurns > 0)
        {
            ActiveTurn = turns[activeTurnIndex];
            solarPointer += 5;
        }
    }
}
