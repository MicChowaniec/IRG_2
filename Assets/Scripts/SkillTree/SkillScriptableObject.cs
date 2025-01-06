using UnityEngine;

[CreateAssetMenu(fileName = "SkillScriptableObject", menuName = "Scriptable Objects/Skill")]
public class SkillScriptableObject : ScriptableObject
{
    public string label;
    public ActionTypeEnum actionType;
    public string description;

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
