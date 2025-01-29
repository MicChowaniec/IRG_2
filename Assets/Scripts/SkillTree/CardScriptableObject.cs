using UnityEngine;

[CreateAssetMenu(fileName = "CardScriptableObject", menuName = "Card")]
public class CardScriptableObject : ScriptableObject
{
    public GameObject skillNotRooted;
    public SkillScriptableObject skillNotRootedSC;
    public Turn turnNotRooted;
    public ActionTypeEnum passiveRooted;

    public GameObject skillRooted;
    public SkillScriptableObject skillRootedSC;
    public Turn turnRooted;
    public ActionTypeEnum passiveNotRooted;
    
    
}
