using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public abstract class Skill : MonoBehaviour, ISkill
{
    public GameAction GameAction;
    private GameObject player;
    public TurnBasedSystem tbs;
    public ScoreKeeper scoreKeeper;

    public void Start()
    {
        tbs = FindAnyObjectByType<TurnBasedSystem>();
        player = tbs.activePlayer;
    }

    void PerformAction(bool movement, bool objectRequied, GameObjectTypeEnum obj1, GameObjectTypeEnum obj2, float range, bool targetSelf)
    {

        scoreKeeper.UpdateResources(GameAction.EnergyChange, GameAction.WaterChange, GameAction.BioMassChange, GameAction.StarlingChange, GameAction.DiseaseChange);

    }
   
        void OnClick()
    {
        //NeedsToChange
        PerformAction(GameAction.Move,  GameAction.NeedsObject, GameAction.ObjectType1, GameAction.ObjectType2, GameAction.Range, GameAction.TargetSelf);
    }

  
}
