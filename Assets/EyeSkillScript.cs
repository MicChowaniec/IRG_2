using UnityEngine;

public class EyeSkillScript : AbstractSkill
{
    public override void Do()
    {
        activePlayer.eyes++;
        StatisticChange();

        Confirm();

        DisableFunction();
    }
}
