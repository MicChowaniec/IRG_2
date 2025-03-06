
using UnityEngine;
using System.Collections.Generic;

using System;
using JetBrains.Annotations;

public class AI : MonoBehaviour
{
    public Turn ActiveTurn;
    public List<SkillScriptableObject> SkillList = new();
    public SkillScriptableObject skipTurn;
    public ActionManager actionManager;
    public PlayerManager playerManager;
    private SkillScriptableObject tempSkill;
    private int rootingPressure;
    public GameSettings gameSeetings;


    public static event Action EndTurn;
    public static event Action<Player, SkillScriptableObject> SendMeAField;
    public static event Action Prepare;
    public static event Action<OnHoverSC> Execute;
    public static event Action ExecuteSelf;

    private void OnEnable()
    {
        
        TurnBasedSystem.CurrentTurnBroadcast += UpdateTurn;
        PlayerScript.AITurn += CalculateMove;
        StarlingPrefabScript.FinallyReachedTheDestination += ExecuteAction;
        StarlingSkillScript.AnimationObjectDestroyed += CalculateMove;
        VisionSystem.FoundAttractiveField += ExecuteAction;
    }

   
    private void OnDisable()
    {
        TurnBasedSystem.CurrentTurnBroadcast -= UpdateTurn;
        PlayerScript.AITurn -= CalculateMove;
        StarlingPrefabScript.FinallyReachedTheDestination -= ExecuteAction;
        StarlingSkillScript.AnimationObjectDestroyed -= CalculateMove;
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
            if (!computer.rooted)
            {
                SkillList.Add(card.skillNotRootedSC);

            }
            else if (computer.rooted)
            {
                SkillList.Add(card.skillRootedSC);
            }
        }
   

    }
    private void CalculateMove(Player computer)
    {
       if(computer.human)
        {
            return;
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
        rootingPressure++;

    }

    private void SkipTurn()
    {
        tempSkill = null;
        EndTurn?.Invoke();
    }





}
