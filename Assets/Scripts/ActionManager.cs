using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionManager : MonoBehaviour
{
    public List<Button> PurpleButtons = new List<Button>();
  
    public List<Button> BlueButtons = new List<Button>();

    public List<Button> GreenButtons = new List<Button>();

    public List<Button> YellowButtons = new List<Button>();

    public List<Button> OrangeButtons = new List<Button>();

    public List<Button> RedButtons = new List<Button>();

    [SerializeField]
    TurnBasedSystem tbs;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckTurnAndPlayer()
    {
        RefreshButtons();
        if (tbs.ActiveTurn.nameOfTurn == "ThirdEyeTurn")
        {
            
            foreach (Button b in PurpleButtons)
            {
                b.interactable = true;
            }

        }
        else if (tbs.ActiveTurn.nameOfTurn == "ThroatTurn")
        {
 
            foreach (Button b in BlueButtons)
            {
                b.interactable = true;
            }

        }
        else if (tbs.ActiveTurn.nameOfTurn == "HearthTurn")
        {

            foreach (Button b in GreenButtons)
            {
                b.interactable = true;
            }

        }
        else if (tbs.ActiveTurn.nameOfTurn == "SolarTurn")
        {
 
            foreach (Button b in YellowButtons)
            {
                b.interactable = true;
            }

        }
        else if (tbs.ActiveTurn.nameOfTurn == "PowerTurn")
        {

            foreach (Button b in OrangeButtons)
            {
                b.interactable = true;
            }
        }
        else if (tbs.ActiveTurn.nameOfTurn == "RootTurn")
        {

            foreach (Button b in RedButtons)
            {
                b.interactable = true;
            }
        }
        else
        {
            Debug.Log("haven't found name of the turn: " + tbs.ActiveTurn.nameOfTurn);
        }
    }
    public void RefreshButtons()
    {
        foreach (Button b in PurpleButtons)
        {
            b.interactable = false;
        }
        foreach (Button b in BlueButtons)
        {
            b.interactable = false;
        }
        foreach (Button b in GreenButtons)
        {
            b.interactable = false;
        }
        foreach (Button b in YellowButtons)
        {
            b.interactable = false;
        }
        foreach (Button b in OrangeButtons)
        {
            b.interactable = false;
        }
        foreach (Button b in RedButtons)
        {
            b.interactable = false;
        }
    }


}
