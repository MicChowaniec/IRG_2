using UnityEditor;
using UnityEngine;
using System;

public class EndTurn : AbstractSkill
{
    public static event Action EndTurnEvent;
    public void OnClick()
    {
        Do(GameObjectTypeEnum.None, ActionTypeEnum.None);
    }
    public override void Do(GameObjectTypeEnum gote, ActionTypeEnum ate)
    {
        EndTurnEvent?.Invoke();
    }
}
