
using UnityEngine;
using System.Collections.Generic;

using System;
using System.Linq;
using UnityEditor;



public class AI : MonoBehaviour
{
    public Turn ActiveTurn;
    public List<SkillScriptableObject> SkillList = new();
    public SkillScriptableObject skipTurn;
    public ActionManager actionManager;
    public PlayerManager playerManager;
    private SkillScriptableObject tempSkill;


    public static event Action EndTurn;
    public static event Action<Player,SkillScriptableObject> SendMeAField;

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
        SkillList.Clear();
        if (computer.human)
        {
            return;
        }
        
        foreach (var card in computer.cards)
        {
            if (card.turnNotRooted == ActiveTurn && !computer.rooted)
            {
                SkillList.Add(card.skillNotRootedSC);

            }
            else if (card.turnRooted ==ActiveTurn && computer.rooted)
            {
                SkillList.Add(card.skillRootedSC);
            }
        }
        
    }
    private void CalculateMove(Player computer)
    {
        if (computer.human)
        {
            return;
        }
        if (SkillList.Count <= 0)
        {
            FindPossibleMoves(computer);
        }

        int index = new System.Random().Next(0, SkillList.Count);

        tempSkill = SkillList[index];

        if (tempSkill.self == false)
        {
            SendMeAField?.Invoke(computer, tempSkill);
        }
    }


    private void ExecuteAction(TileScriptableObject tso)
    {
        
        
        Debug.Log(playerManager.activePlayer.itsName + " used " + tempSkill.label + " on " + tso.label + ", " + tso.childType.ToString() + ", " + tso.childColor.ToString());
        
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
