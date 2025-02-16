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
		GameObjectTypeEnum gote = tso.GetChildObjectType();
		ActionTypeEnum ate = tso.GetChildObjectColor();
		switch (gote)
		{
			case GameObjectTypeEnum.Player:
				{
					if (onHoverSC.GetStander() != activePlayer)
					{
						int temp = activePlayer.DealDamage();
						onHoverSC.GetStander().ReceiveDamage(temp);
						activePlayer.Grow(temp);
						activePlayer.AddGenom(ate, 1);

						//Place to implement Attitude dependency
					}
					else
					{
						return;
					}
				}
				break;
			case GameObjectTypeEnum.Tree:
				{
					if (ate != ActionTypeEnum.None)
					{
						int temp = activePlayer.DealDamage();
						onHoverSC.GetStandingTree().ReceiveDamage(temp);
						activePlayer.Grow(temp);
						activePlayer.AddGenom(ate, 1);
					}
				}
				break;
			case GameObjectTypeEnum.Bush:
				{
					activePlayer.Grow(1);
					activePlayer.AddGenom(ate, 1);
				}
				break;
			case GameObjectTypeEnum.Water:
				{
					activePlayer.WaterLoss(-100);

				}
				break;
			case GameObjectTypeEnum.None:
				{
					return;
				}


		}
		StatisticChange();
		Confirm();

	}
}
