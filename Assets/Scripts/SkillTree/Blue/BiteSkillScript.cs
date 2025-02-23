

public class BiteSkillScript : AbstractSkill
{


	public override void ClickOnButton()
	{
	  
	}

	public override void Confirm()
	{

	}

	public override void Do(OnHoverSC onHoverSC)
	{
		if (tso!=null)
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
						activePlayer.Refill();

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
}
