using UnityEngine;
using System;
using Unity.VisualScripting;
using System.Runtime.CompilerServices;
using UnityEditor;

public class StarlingSkillScript : AbstractSkill
{


    public GameObject StarlingPrefab;
    public GameObject StarlingInstantiated;
    private Vector3 scanningCenter;

    
    public static event Action<Player,Vector3,int> SetNest;
    public static event Action<Player> BirdDestroyed;

    public override void CheckColorIncome(OnHoverSC tso)
    {
        scanningCenter = tso.GetPosition();
        clickedTileObject = tso.GetChildObjectType();
        clickedTileColor = tso.GetChildObjectColor();
    }

    public override void Do(GameObjectTypeEnum gote, ActionTypeEnum ate)
    {

        switch (clickedTileObject)
        {
            case GameObjectTypeEnum.Water:
                {
                    activePlayer.Grow(2);
                    
                    Debug.Log("Fish Eaten");

                    break;
                }
            case GameObjectTypeEnum.Bush:
                {
                    activePlayer.AddGenom(clickedTileColor, 1);
                    activePlayer.Grow(1);
                    Debug.Log("Genom Collected: " + clickedTileColor);

                    break;

                }
            case GameObjectTypeEnum.Rock:
                {
                    SetNest?.Invoke(activePlayer,scanningCenter,3);

                    break;
                }
            case GameObjectTypeEnum.Tree:
                {
                    if (clickedTileColor != ActionTypeEnum.None)
                    {
                        SetNest?.Invoke(activePlayer,scanningCenter,3);
                    }
                    else
                    {
                        activePlayer.AddGenom(clickedTileColor, 1);
                        activePlayer.Grow(1);
                        Debug.Log("Genom Collected: " + clickedTileColor);
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
        StatisticChange(-1, 0, 0, 0, 0, 0);
        Confirm();
    }
    public override void ClickOnButton()
    {
       
        if (CheckResources(1,0,0,0,0,0)&&!thisListener)
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
