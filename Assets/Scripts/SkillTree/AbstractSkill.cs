using JetBrains.Annotations;
using UnityEngine;

public class AbstractSkill : MonoBehaviour
{
    public SkillScriptableObject skill;
    public virtual void StatisticChange(int starling, int biomass, int water, int energy, int protein, int resistance, int eyes)
    {

    }
    public virtual void Do(int range, bool self)
    {

    }
    

}
