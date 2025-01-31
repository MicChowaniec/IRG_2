using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine.UIElements;
using System;
using System.Linq;
using System.Reflection.Emit;

public class AI : MonoBehaviour
{
    public Turn ActiveTurn;
    public List<SkillScriptableObject> SkillList = new();
    public SkillScriptableObject skipTurn;
    public ActionManager actionManager;
    public PlayerManager playerManager;

    private void OnEnable()
    {

        TurnBasedSystem.CurrentTurnBroadcast += UpdateTurn;
        PlayerScript.AITurn += CalculateMove;
    }
    private void OnDisable()
    {
        TurnBasedSystem.CurrentTurnBroadcast -= UpdateTurn;
        PlayerScript.AITurn -= CalculateMove;
    }
    private void FindPossibleMoves(Player computer)
    {
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

        FindPossibleMoves(computer);


        int index = new System.Random().Next(0, SkillList.Count);

        SkillScriptableObject tempSkill = SkillList[index];
        Debug.Log(tempSkill);
        GameObjectTypeEnum tempType = GameObjectTypeEnum.None;
        Debug.Log(tempType);
        ActionTypeEnum tempColor = ActionTypeEnum.None;
        Debug.Log(tempColor);

        if (tempSkill.label == "Starling")
        {

            tempType = GameObjectTypeEnum.Rock;
            TileScriptableObject destinationField = DestinationField(computer, tempSkill.label, tempType, tempColor);
            if (destinationField == null)
            {
                tempType = GameObjectTypeEnum.Bush;
                tempColor = AskForColor(computer);

                destinationField = DestinationField(computer, tempSkill.label, tempType, tempColor);
                if (destinationField ==null)
                {
                    tempType = GameObjectTypeEnum.Water;

                    destinationField = DestinationField(computer, tempSkill.label, tempType, tempColor);
                    if(destinationField==null)
                    {
                        // Add attack behaviour
                        SkipTurn();

                        Debug.Log(computer.itsName + " used " + tempSkill.label + " on " + destinationField.label + ", " + tempType.ToString() + ", " + tempColor.ToString());
                        return;
                        
                    }
                }
            }
            StarlingSkillScript sss = actionManager.GetComponent<StarlingSkillScript>();
            sss.ClickOnButton();
            sss.StarlingInstantiated.GetComponent<StarlingPrefabScript>().AIStarlingMovememnt(destinationField);
            sss.Do(0, false);
            Debug.Log(computer.itsName + " used " + tempSkill.label + " on " + destinationField.label + ", " + tempType.ToString() + ", " + tempColor.ToString());
            SkipTurn();
        }
        // Implement All Skills
    }

    private TileScriptableObject DestinationField(Player computer, string label, GameObjectTypeEnum gameObjectTypeEnum, ActionTypeEnum actionType)
    {
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
        playerManager.ChangePlayer();
    }





}
