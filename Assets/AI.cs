
using UnityEngine;
using System.Collections.Generic;

using System;
using System.Linq;



public class AI : MonoBehaviour
{
    public Turn ActiveTurn;
    public List<SkillScriptableObject> SkillList = new();
    public SkillScriptableObject skipTurn;
    public ActionManager actionManager;
    public PlayerManager playerManager;
    public StarlingSkillScript sss;
    private SkillScriptableObject tempSkill;
    

    public static event Action EndTurn;

    private void OnEnable()
    {

        TurnBasedSystem.CurrentTurnBroadcast += UpdateTurn;
        PlayerScript.AITurn += CalculateMove;
        StarlingPrefabScript.FinallyReachedTheDestination += ExecuteAction;
        StarlingSkillScript.BirdDestroyed += CalculateMove;
    }
    private void OnDisable()
    {
        TurnBasedSystem.CurrentTurnBroadcast -= UpdateTurn;
        PlayerScript.AITurn -= CalculateMove;
        StarlingPrefabScript.FinallyReachedTheDestination -= ExecuteAction;
        StarlingSkillScript.BirdDestroyed += CalculateMove;

    }
    private void FindPossibleMoves(Player computer)
    {
        if(computer.human)
        {
            return;
        }
        SkillList.Clear();
        foreach (var card in computer.cards)
        {
            if (card.turnNotRooted == ActiveTurn && !computer.rooted)
            {
                SkillList.Add(card.skillNotRootedSC);
                Debug.Log(SkillList.First().label);

            }
            else if (card.turnRooted && computer.rooted)
            {
                SkillList.Add(card.skillRootedSC);
                Debug.Log(SkillList);
            }
        }
        
    }
    private void CalculateMove(Player computer)
    {
        if (computer.human)
        {
            return;
        }
        FindPossibleMoves(computer);


        int index = new System.Random().Next(0, SkillList.Count);

        tempSkill = SkillList[index];
        Debug.Log(tempSkill);
        GameObjectTypeEnum tempType = GameObjectTypeEnum.None;
        Debug.Log(tempType);
        ActionTypeEnum tempColor = ActionTypeEnum.None;
        Debug.Log(tempColor);

        if (tempSkill.label == "Starling")
        {
            if (computer.starlings <= 0)
            {
                SkillList.Remove(tempSkill);
                SkipTurn();
                return;
            }
            tempType = GameObjectTypeEnum.Rock;
            TileScriptableObject destinationField = DestinationField(computer, tempSkill.label, tempType, tempColor);
            if (destinationField == null)
            {
                Debug.Log(computer.itsName + "Don't see a rock");
                tempType = GameObjectTypeEnum.Bush;
                tempColor = AskForColor(computer);

                destinationField = DestinationField(computer, tempSkill.label, tempType, tempColor);
                if (destinationField == null)
                {
                    Debug.Log(computer.itsName + "Don't see a bush");
                    tempType = GameObjectTypeEnum.Water;

                    destinationField = DestinationField(computer, tempSkill.label, tempType, tempColor);
                    if (destinationField == null)
                    {
                        Debug.Log(computer.itsName + "Don't see a shit");
                        // Add attack behaviour
                        SkipTurn();


                        return;

                    }
                }
            }
            
            sss = actionManager.GetComponent<StarlingSkillScript>();
            sss.ClickOnButton();
            sss.StarlingInstantiated.GetComponent<StarlingPrefabScript>().AIStarlingMovememnt(destinationField);
           
        }
        // Implement All Skills
    }
    private void ExecuteAction(TileScriptableObject tso)
    {
        
        sss.Do(tso.childType, tso.childColor);
        Debug.Log(playerManager.activePlayer.itsName + " used " + tempSkill.label + " on " + tso.label + ", " + tso.childType.ToString() + ", " + tso.childColor.ToString());
        
    }
    private TileScriptableObject DestinationField(Player computer, string label, GameObjectTypeEnum gameObjectTypeEnum, ActionTypeEnum actionType)
    {
        Debug.Log("Asking Vision System");
        return playerManager.GetGameObjectFromSO(computer).GetComponent<VisionSystem>().FindAttractiveField(label, gameObjectTypeEnum, actionType);
    }

    private ActionTypeEnum AskForColor(Player computer)
    {
        return playerManager.GetGameObjectFromSO(computer).GetComponent<VisionSystem>().AskForColor();
    }


    private void UpdateTurn(Turn turn)
    {
        ActiveTurn = turn;
    }

    private void SkipTurn()
    {
        EndTurn?.Invoke();
    }





}
