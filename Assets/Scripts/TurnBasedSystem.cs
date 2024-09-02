using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBasedSystem : MonoBehaviour
{
    [SerializeField]
    private List<Turn> turns = new List<Turn>(); // List of turns, serialized for Unity Editor
    public Turn ActiveTurn { get; private set; } // Property to access the active turn
    private int activeTurnIndex;
    private int numberOfTurns;
    [SerializeField]
    public Button firstAction;
    public Button secondAction;
    public Button thirdAction;
    public Button fourthAction;

    void Start()
    {
        numberOfTurns = turns.Count;
        activeTurnIndex = 0;
        if (numberOfTurns > 0)
        {
            ActiveTurn = turns[activeTurnIndex];
        }
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
        // TODO implement buttons managament for turns
    }
}
