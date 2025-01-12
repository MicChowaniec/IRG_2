using UnityEditor;
using UnityEngine;
using System;

public class EndTurn : AbstractSkill
{
    public static event Action EndTurnEvent;
    public void OnClick()
    {
        Do(0,false);
    }
    public override void Do(int range, bool self)
    {
        EndTurnEvent?.Invoke();
    }
}
