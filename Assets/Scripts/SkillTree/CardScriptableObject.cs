using UnityEngine;

[CreateAssetMenu(fileName = "CardScriptableObject", menuName = "Card")]
public class CardScriptableObject : ScriptableObject
{
    public ScriptableObject skillNotRooted;
    public ScriptableObject skillRooted;
    public ScriptableObject passiveEffect;
}
