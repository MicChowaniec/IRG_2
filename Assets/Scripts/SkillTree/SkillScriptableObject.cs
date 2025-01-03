using UnityEngine;

[CreateAssetMenu(fileName = "SkillScriptableObject", menuName = "Scriptable Objects/Skill")]
public class SkillScriptableObject : ScriptableObject
{
    public string label;
    public ActionTypeEnum action;
    public string description;


}
