
using System;
using System.Collections.Generic;

using UnityEngine;

public class ActionManager : MonoBehaviour
{
   
    public List<GameObject> purpleSkills = new();
    public List<GameObject> blueSkills = new();
    public List<GameObject> greenSkills = new();
    public List<GameObject> yellowSkills = new();
    public List<GameObject> orangeSkills = new();
    public List<GameObject> redSkills = new();
    private List<List<GameObject>> AllSkills => new ()
    {
        purpleSkills,
        blueSkills,
        greenSkills,
        yellowSkills,
        orangeSkills,
        redSkills
    };

    

    private Turn activeTurn;
    public static event Action SkillsUpdated;

    private void OnEnable()
    {  
        TurnBasedSystem.CurrentTurnBroadcast += ActiveTurn;
    }
    private void OnDisable()
    {
        TurnBasedSystem.CurrentTurnBroadcast -= ActiveTurn;
    }

    private void ActiveTurn(Turn turn)
    {
        Reset();
        activeTurn = turn;
        switch (activeTurn.actionType)
        {
            case ActionTypeEnum.Purple:
                {
                    
                    foreach (GameObject obj in purpleSkills)
                    {
                        obj.SetActive(true);
                    }
                    break;
                }
            case ActionTypeEnum.Blue:
                {
                    foreach (GameObject obj in blueSkills)
                    {
                        obj.SetActive(true);
                    }
                    break;
                }

            case ActionTypeEnum.Green:
                {
                    foreach(GameObject obj in greenSkills)
                    {
                        obj.SetActive(true);
                    }
                    break;
                }
            case ActionTypeEnum.Yellow:
                {
                    foreach(GameObject obj in yellowSkills)
                    {
                            obj.SetActive(true);
                    }
                    break;
                }
            case ActionTypeEnum.Orange:
                {
                    foreach(GameObject obj in orangeSkills)
                    {
                        obj.SetActive(true);
                    }
                    break;
                }
            case ActionTypeEnum.Red:
                {
                    foreach(GameObject obj in redSkills)
                    {
                        obj.SetActive(true);
                    }
                    break;
                }

        }
        SkillsUpdated?. Invoke();
    }
    private void Reset()
    {
        foreach (List<GameObject> list in AllSkills)
        { 
            foreach (GameObject obj in list)
                {
                obj.SetActive(false);
                }
        }
    }


}
