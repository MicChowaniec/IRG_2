using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "SkillScriptableObject", menuName = "Skill")]
public class SkillScriptableObject : OnHoverSC
{
    public ActionTypeEnum actionType;
    public bool RootedSkill;

    
    public GameObject ButtonPrefab;

    public int starling;
    public int biomass;
    public int water;
    public int energy;
    public int protein;
    public int resistance;
    public int eyes;

    public int range;
    public bool self;



}
