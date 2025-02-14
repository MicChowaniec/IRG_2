using UnityEngine;
using System;
using Unity.VisualScripting;
using System.Runtime.CompilerServices;
using UnityEditor;

public class StarlingSkillScript : AbstractSkill
{


    public GameObject StarlingPrefab;
    public GameObject StarlingInstantiated;


    
    public static event Action<Player,Vector3,int> SetNest;
    public static event Action<Player> BirdDestroyed;

    public override void Do(OnHoverSC tso)
    {
        GameObjectTypeEnum gote = tso.GetChildObjectType();
        ActionTypeEnum ate = tso.GetChildObjectColor();
        Vector3 scanningCenter = tso.GetPosition();

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
                    SetNest?.Invoke(activePlayer,scanningCenter,3);

                    break;
                }
            case GameObjectTypeEnum.Tree:
                {
                    if (tso.GetChildObjectColor() != ActionTypeEnum.None)
                    {
                        SetNest?.Invoke(activePlayer,scanningCenter,3);
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

                    return;
                }
            case GameObjectTypeEnum.None:
                {
                    return;
                }


        }
        StatisticChange();
        Confirm();
    }
    public override void ClickOnButton()
    {
       
        if (CheckResources())
        {
            StarlingInstantiated = Instantiate(StarlingPrefab, activePlayer.Pos, Quaternion.identity);
            
            Cursor.visible = false; // Hide the cursor
            ThisListener(true);
        }
    }
   
    public override void Confirm()
    {

        ThisListener(false);

        if (StarlingInstantiated != null)
        {
            Destroy(StarlingInstantiated);
            StarlingInstantiated = null;
        }
        Cursor.visible = true;
        BirdDestroyed?.Invoke(activePlayer);
       

    }






}
