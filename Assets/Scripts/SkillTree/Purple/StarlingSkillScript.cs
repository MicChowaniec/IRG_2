using UnityEngine;
using System;


public class StarlingSkillScript : AbstractSkill
{

    public static event Action<Player,Vector3,int> SetNest;

    public static event Action<Player> UpdateVision;

    

    public override void Do(TileScriptableObject tso)
    {
      
        Debug.Log("Starting Doing Skill");
      
            GameObjectTypeEnum gote = tso.GetChildObjectType();
            ActionTypeEnum ate = tso.GetChildObjectColor();
            Vector3 scanningCenter = tso.GetPosition();
            Debug.Log(gote + ", " + ate);
        switch (gote)
        {
            case GameObjectTypeEnum.Water:
                {
                    activePlayer.Grow(2);

                    Debug.Log("Fish Eaten");

                    break;
                }
            case GameObjectTypeEnum.Bush:
                {

                    activePlayer.AddGenom(ate, 1);
                    activePlayer.Grow(1);
                    Debug.Log("Genom Collected: " + ate);

                    break;

                }
            case GameObjectTypeEnum.Rock:
                {
                    SetNest?.Invoke(activePlayer, scanningCenter, 3);
                    Debug.Log("Nest Set");
                    break;
                }
            case GameObjectTypeEnum.Tree:
                {
                    if (tso.GetChildObjectColor() != ActionTypeEnum.None)
                    {
                        SetNest?.Invoke(activePlayer, scanningCenter, 3);
                    }
                    else
                    {
                        activePlayer.AddGenom(ate, 1);
                        activePlayer.Grow(1);
                        Debug.Log("Genom Collected: " + ate);
                    }
                    break;
                }
            case GameObjectTypeEnum.Player:
                {
                    Player target = tso.GetStander();
                    if (target != activePlayer)
                    {
                        if (target.eyes >= 1)
                        {
                            target.eyes--;
                            Debug.Log("Player: " + ate.ToString() + " blinded.");
                        }

                    }
                    else
                    {
                        target.eyes++;
                        UpdateVision?.Invoke(activePlayer);
                        Debug.Log("You have got an additional eye");

                    }
                    break;
                }

        }
        StatisticChange();

        Confirm();
        
        DisableFunction();

    }

}

