using System;
using UnityEngine;

public class BiteSkillScript : AbstractSkill
{


    public override void ClickOnButton()
    {
       if(CheckResources())
        {
            ThisListener(true);
        }

       
    }

    public override void Confirm()
    {
        ThisListener(false);
    }
    public override void Do(OnHoverSC onHoverSC)
    {
       
    }
}
