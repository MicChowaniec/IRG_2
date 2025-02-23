
using UnityEngine;
using System.Collections.Generic;

using System;




public class AI : MonoBehaviour
{
    public Turn ActiveTurn;
    public List<SkillScriptableObject> SkillList = new();
    public SkillScriptableObject skipTurn;
    public ActionManager actionManager;
    public PlayerManager playerManager;
    private SkillScriptableObject tempSkill;


    public static event Action EndTurn;
    public static event Action<Player, SkillScriptableObject> SendMeAField;
    public static event Action<OnHoverSC> Execute;
    public static event Action ExecuteSelf;

    private void OnEnable()
    {

        TurnBasedSystem.CurrentTurnBroadcast += UpdateTurn;
        PlayerScript.AITurn += CalculateMove;
        StarlingPrefabScript.FinallyReachedTheDestination += ExecuteAction;
        StarlingSkillScript.BirdDestroyed += CalculateMove;
        VisionSystem.FoundAttractiveField += ExecuteAction;
    }

   
    private void OnDisable()
    {
        TurnBasedSystem.CurrentTurnBroadcast -= UpdateTurn;
        PlayerScript.AITurn -= CalculateMove;
        StarlingPrefabScript.FinallyReachedTheDestination -= ExecuteAction;
        StarlingSkillScript.BirdDestroyed -= CalculateMove;
        VisionSystem.FoundAttractiveField -= ExecuteAction;

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
            else if (card.turnRooted == ActiveTurn && computer.rooted)
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
  
            FindPossibleMoves(computer);

        if(SkillList.Count==0)
        {
            SkipTurn();
        }

        int index = new System.Random().Next(0, SkillList.Count);
        
        tempSkill = SkillList[index];

        if (tempSkill.self == false)
        {
            SendMeAField?.Invoke(computer, tempSkill);
        }
        else if (tempSkill.self == true)
        {
            ExecuteAction();
        }

    }


    private void ExecuteAction(TileScriptableObject tso )
    {

        Execute?.Invoke(tso);
        Debug.Log(playerManager.activePlayer.itsName + " used " + tempSkill.label + " on " + tso.label + ", " + tso.GetChildObjectType() + ", " + tso.GetChildObjectColor());

    }
    private void ExecuteAction()
    {
        ExecuteSelf?.Invoke();
        Debug.Log(playerManager.activePlayer.itsName + " used " + tempSkill.label);
    }


    private void UpdateTurn(Turn turn)
    {
        ActiveTurn = turn;
    }

    private void SkipTurn()
    {
        tempSkill = null;
        EndTurn?.Invoke();
    }





}
