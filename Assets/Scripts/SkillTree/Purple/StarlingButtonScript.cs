using UnityEngine;
using UnityEngine.UI;

public class StarlingButtonScript : ButtonScript
{


    public override void OnClick()
    {
        ActionManager.GetComponent<StarlingSkillScript>().ClickOnButton();
    }
    

}
