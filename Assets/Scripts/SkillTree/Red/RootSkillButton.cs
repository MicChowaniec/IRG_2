using UnityEngine;
using UnityEngine.UI;

public class RootSkillButton :ButtonScript
{
    public void Update()
    {
        if(tileWherePlayerStands.rootable!=false||!CheckResources())
        {
            this.GetComponent<Button>().interactable = false;
        }
        else
        {
            this.GetComponent<Button>().interactable = true;
        }
    }
}
