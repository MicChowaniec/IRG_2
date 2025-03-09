using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
public class StarlingSkillButton : ButtonScript
{
    public void Update()
    {
        if (activePlayer.human)
        {
            if (!CheckResources())
            {
                this.GetComponent<Button>().interactable = false;
            }
            else 
            {
                this.GetComponent<Button>().interactable = true;
            }
        }

    }
}
