using UnityEngine;

public class BasicPhotoSynthetise :AbstractSkill
{
    public override void Do()
    {
        activePlayer.Grow(2);

        StatisticChange();

        Confirm();

        DisableFunction();
    }

}
