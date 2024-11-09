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
    public ScoreKeeper scoreKeeper;

    private int solarPointer;
    private int solarMaximum;
    private int signature;

    [Range(1, 15)]
    public int step;


    public GameObject dayUI;

    public GameObject nightUI;
  
    public Light Light;


    public List<GameObject> players;
    public GameObject activePlayer;
    public GameObject pickedPlayer;
    private int numberOfPlayers;
    private int activePlayerIndex;

 
    public MovementSystem ms;


    public int totalTurns;


    public Image image;


    public StrategyCameraControl scc;

    public ActionManager am;

    public int diseaseLevel;

    void Start()
    {
        totalTurns = 0;
        signature = 1;
        solarMaximum = 45;

        SolarRise();

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
        am.CheckTurnAndPlayer();
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
        if (activePlayer.GetComponent<PlayerScript>().picked==true)
        {
            scc.CenterOnObject(pickedPlayer);
            
            activePlayer.GetComponent<PlayerScript>().UpdateEnergy(0);
            activePlayer.GetComponent<PlayerScript>().UpdateWater(0);
            activePlayer.GetComponent<PlayerScript>().UpdateStarlings(0);
            activePlayer.GetComponent<PlayerScript>().UpdateBioMass(0);
           // activePlayer.GetComponent<PlayerScript>().UpdateStarlings();
        }



        totalTurns++;

    }
    public void SetNextTurn()
    {
        numberOfTurns = turns.Count;
        activeTurnIndex = (activeTurnIndex + 1) % numberOfTurns;
        ActiveTurn = turns[activeTurnIndex];
        image.sprite = ActiveTurn.icon;
        // Advance to the next turn and loop back if at the end
        solarPointer += step;
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
   
    /// <summary>
    /// Function Called externally by button, or sequence
    /// </summary>
    public void SolarRise()
    {
        dayUI.SetActive(true);
        nightUI.SetActive(false);
        solarPointer = 1;
    }
    /// <summary>
    /// Adjust Sun position and UI to the existing Sun position
    /// </summary>
    public void CheckTurn()

    {
        //Sun position funtion during day/night
        if (solarPointer > 0 && solarPointer < 181)
        {
            float x = Mathf.Abs((float)solarPointer - 1 / 2 * solarMaximum) - 1 / 2 * solarMaximum;
            float y = (float)solarPointer / 2;
            Light.transform.rotation = Quaternion.Euler(new Vector3(x, y, 0));
            am.CheckTurnAndPlayer();
            foreach(GameObject p in players)
            {
                PlayerScript ps = p.GetComponent<PlayerScript>();
                ps.UpdateEnergy((int)x);
                ps.UpdateWater(-6);

            }
            Debug.Log("Energy added:" + x);
        }
        else
        {
            dayUI.SetActive(false);
            nightUI.SetActive(true);
            solarPointer = -90;
            float x = -90;
            float y = (float)solarPointer / 2;
            Light.transform.rotation = Quaternion.Euler(new Vector3(x, y, 0));
        }


    }
    public void EndNightButton()
    {
        nightUI.SetActive(false);
        dayUI.SetActive(true);
        solarPointer = 1;
        if (activePlayer.GetComponent<PlayerScript>().rooted == true)
        {

            Destroy(activePlayer);
            
        }
    }
}
