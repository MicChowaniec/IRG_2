using JetBrains.Annotations;
using UnityEngine;

public class StarlingSkillButton : ButtonScript
{
    private int iterations = 0;
    public override void OnClick()
    {
        iterations++;
        if (ActionManager == null)
        {
            ActionManager = FindAnyObjectByType<ActionManager>();
        }
        if (ActionManager != null)
        {
            ActionManager.GetComponentInChildren<StarlingSkillScript>().ClickOnButton();
            Debug.Log(skill.name + "executed at iteration: " + iterations);
        }
        
        else if(iterations<5) 
        {
            OnClick();
        }
        else
        {
            Debug.Log(skill.name + "cannot be executed");
        }

    }
}
