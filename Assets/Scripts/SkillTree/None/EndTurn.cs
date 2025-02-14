using UnityEditor;
using UnityEngine;
using System;

public class EndTurn : AbstractSkill
{
    
    public static event Action EndTurnEvent;
    public void OnClick()
    {
        Do();
    }
    public override void Do()
    {
        EndTurnEvent?.Invoke();
    }
}
