using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class AI : MonoBehaviour
{
    private Turn Activeturn;
    private SkillScriptableObject[] SkillList = new SkillScriptableObject[6];
    private void OnEnable()
    {
        TurnBasedSystem.currentTurnBroadcast += UpdateTurn;
        PlayerScript.AITurn += CalculateMove;
    }
    private void OnDisable()
    {
        TurnBasedSystem.currentTurnBroadcast -= UpdateTurn;
        PlayerScript.AITurn -= CalculateMove;
    }
    private void FindPossibleMoves(Player computer)
    {
        int i = 0;
        foreach (var card in computer.cards)
        {
            if (card.turnNotRooted == Activeturn && !computer.rooted)
            {
                SkillList[i] = card.skillNotRootedSC;
                i++;
            }
            else if (card.turnRooted && computer.rooted)
            {
                SkillList[i] = card.skillRootedSC;
            }
        }
    }
    private void CalculateMove(Player computer)
    {
        FindPossibleMoves(computer);

    }

    private void UpdateTurn(Turn turn)
    {
        Activeturn = turn;
    }


}
