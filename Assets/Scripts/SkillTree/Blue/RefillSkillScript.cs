using UnityEngine;
using UnityEngine.Rendering;

public class RefillSkillScript : AbstractSkill
{
    public override void Do()
    {
      
                    activePlayer.Refill();
                    StatisticChange();

                    Confirm();

                    DisableFunction();
                    
                
    }
}
