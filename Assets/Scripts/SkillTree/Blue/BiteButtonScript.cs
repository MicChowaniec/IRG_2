using UnityEngine;

public class BiteButtonScript: ButtonScript
{


    public override void OnClick()
    {
        
        ActionManager.GetComponent<BiteSkillScript>().ClickOnButton();
    }


}
