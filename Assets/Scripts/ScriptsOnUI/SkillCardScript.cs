using TMPro;
using UnityEngine;

public class SkillCardScript : MonoBehaviour
{
    public CardScriptableObject card;
    public GameObject skillUnrooted;
    public GameObject skillRooted;
    public TextMeshProUGUI passiveUnrootedLabel;
    public TextMeshProUGUI passiveRootedLabel;
    public TextMeshProUGUI passiveUnrootedDescription;
    public TextMeshProUGUI passiveRootedDescription;

    public void FillTheUI()
    {
        InstantiateCard(card.skillRooted, card.skillNotRooted, card.passiveNotRooted, card.passiveRooted);
    }
    private void InstantiateCard(GameObject notRooted, GameObject rooted, ActionTypeEnum passiveNotRooted, ActionTypeEnum passiveRooted)
    {
        Instantiate(notRooted, skillUnrooted.transform);
        Instantiate(rooted, skillRooted.transform);
        passiveUnrootedLabel.text = passiveNotRooted.ToString();
        passiveRootedLabel.text = passiveRooted.ToString();
        passiveUnrootedDescription.text = notRooted.GetComponent<AbstractSkill>().skill.ReturnPassiveDescription(passiveNotRooted);
        passiveRootedDescription.text = rooted.GetComponent<AbstractSkill>().skill.ReturnPassiveDescription(passiveRooted);

    }
}
