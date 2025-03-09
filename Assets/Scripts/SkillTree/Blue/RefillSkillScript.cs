using UnityEngine;
using UnityEngine.Rendering;

public class RefillSkillScript :AbstractSkill
{
    public override void Do()
    {
        if(tileWherePlayerStands!=null)
        {
            foreach (var neigh in tileWherePlayerStands.neighbours)
            {
                if (neigh.gote == GameObjectTypeEnum.Water)
                {
                    activePlayer.Refill();
                    StatisticChange();

                    Confirm();

                    DisableFunction();
                    return;
                }
            }

        }

    }
}
