
using UnityEngine;

[CreateAssetMenu(fileName = "Turn", menuName = "Add Turn", order = 2)]
public class Turn : ScriptableObject
{
    public Sprite icon;
    public string nameOfTurn;
    public string description;
    public ActionTypeEnum actionType;
    public SkillScriptableObject SpecialSkill;
}
