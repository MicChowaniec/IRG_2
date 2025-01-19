using UnityEngine;

[CreateAssetMenu(fileName = "CardScriptableObject", menuName = "Card")]
public class CardScriptableObject : ScriptableObject
{
    public GameObject skillNotRooted;
    public GameObject skillRooted;
    public bool passive;
    public bool passiveFromRooted;
}
