using JetBrains.Annotations;
using UnityEngine;
using System;
using UnityEditor;

public class AbstractSkill : MonoBehaviour
{
    public SkillScriptableObject skill;
    public Player activePlayer;
    public virtual void StatisticChange(int starling, int biomass, int water, int energy, int protein, int resistance, int eyes)
    {

    }
    public virtual void Do(GameObjectTypeEnum gote, ActionTypeEnum ate)
    {

    }
    public virtual bool CheckResources(int res)
    {
        return false;
    }
    
    


}
