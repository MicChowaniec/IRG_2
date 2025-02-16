using UnityEngine;

public class BiteSkillButton : ButtonScript
{
    public override void OnClick()
    {
        ActionManager.GetComponentInChildren<StarlingSkillScript>().ClickOnButton();
    }
}
