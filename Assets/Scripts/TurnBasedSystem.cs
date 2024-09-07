using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TurnBasedSystem : MonoBehaviour
{
    [SerializeField]
    public List<Turn> turns = new List<Turn>(); // List of turns, serialized for Unity Editor
    
    public Turn ActiveTurn { get; private set; } // Property to access the active turn
    public int activeTurnIndex; //Value between 0 and Length of turns
    private int numberOfTurns;

    private int solarPointer;
    private int solarMaximum;
    private int signature;

    [SerializeField]
    public GameObject dayUI;
    [SerializeField]
    public GameObject nightUI;
    [SerializeField]
    public Light Light;

    [SerializeField]
    public List<GameObject> players = new List<GameObject>();
    public GameObject activePlayer;
    private int numberOfPlayers;
    private int activePlayerIndex;

    public int totalTurns;

    void Start()
    {
        totalTurns = 0;

        SolarRise();
        numberOfTurns = turns.Count;
        activeTurnIndex = 0;

        numberOfPlayers = players.Count;
        activePlayerIndex = 0;
        solarMaximum = 45;
        

        CheckTurn();
    }

    void Update()
    {
      

    }
    public void ChangePlayer()
    {
        activePlayerIndex = (activePlayerIndex + 1) % numberOfPlayers;
    }
    public void SetNextDayTurn()
    {

        // Advance to the next turn and loop back if at the end
        activeTurnIndex = (activeTurnIndex + 1) % numberOfTurns;
        ActiveTurn = turns[activeTurnIndex];
        solarPointer += 5;
        //Sun position check for zenit during year
        if (signature != -1 && solarMaximum < 90)
        {
            signature = 1;
        }
        else 
        {
            signature = -1;
        }

        solarMaximum += 1 * signature;
        CheckTurn();
        // TODO implement buttons managament for turns
    }
    public void SolarSet()
    {
        dayUI.SetActive(false);
        nightUI.SetActive(true);
        solarPointer = -90;
    }
    /// <summary>
    /// Function Called externally by button, or sequence
    /// </summary>
    public void SolarRise()
    {
        dayUI.SetActive(false);
        nightUI.SetActive(true);
        solarPointer = 1;
    }
    public void CheckTurn()

    {
        //Sun position funtion during day/night
        if (solarPointer > 0 && solarPointer < 181)
        {
            float x = Mathf.Abs((float)solarPointer - 1 / 2 * solarMaximum) - 1 / 2 * solarMaximum;
            float y = (float)solarPointer / 2;
            Light.transform.rotation = Quaternion.Euler(new Vector3(x, y, 0));
        }
        else
        {
            SolarSet();
        }

    }
}
