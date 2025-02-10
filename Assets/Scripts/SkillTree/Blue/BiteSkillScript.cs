using System;
using UnityEngine;

public class BiteSkillScript : AbstractSkill
{


    public override void ClickOnButton()
    {
       if(CheckResources(0, 0, 0, 0, 10, 0))
        {
            ThisListener(true);
        }

       
    }
    public override void CheckColorIncome(OnHoverSC tso)
    {
        if (tso.GetChildObjectType() != GameObjectTypeEnum.None)
        {
            clickedTileObject = tso.GetChildObjectType();
            clickedTileColor = tso.GetChildObjectColor();
            
        }
        else
        {
            return;
        }
    }

    public override void Confirm()
    {
        ThisListener(false);
    }
    public override void Do(GameObjectTypeEnum gote, ActionTypeEnum ate)
    {
       
    }
}
