using UnityEngine;
using System;
using Unity.VisualScripting;
using System.Runtime.CompilerServices;
using UnityEditor;

public class StarlingSkillScript : AbstractSkill
{


    public GameObject StarlingPrefab;
    public GameObject StarlingInstantiated;
    private bool birdActive;    
    private Vector3 ScanningCenter;

    
    public static event Action<Player,Vector3,int> SetNest;
    public static event Action<Player> BirdDestroyed;





    public void CheckColorIncome(string arg1, string arg2, Vector3 position, GameObjectTypeEnum type, ActionTypeEnum color)
    {
        ScanningCenter = position;
        clickedTileObject = type;
        Debug.Log("Tile Object: " + type);
        clickedtileColor = color;
        Debug.Log("Object Color: " + color);
    }

    private void SetActiveBird(bool b)
    {
        birdActive = b;
        if (b&&activePlayer.human)
        {
            OnHoverScript.OnHoverBroadcast += CheckColorIncome;
        }
        if(!b)
        {
            OnHoverScript.OnHoverBroadcast -= CheckColorIncome;
        }
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
                    activePlayer.AddGenom(clickedtileColor, 1);
                    activePlayer.Grow(1);
                    Debug.Log("Genom Collected: " + clickedtileColor);

                    break;

                }
            case GameObjectTypeEnum.Rock:
                {
                    SetNest?.Invoke(activePlayer,ScanningCenter,3);

                    break;
                }
            case GameObjectTypeEnum.Tree:
                {
                    if (clickedtileColor != ActionTypeEnum.None)
                    {
                        SetNest?.Invoke(activePlayer,ScanningCenter,3);
                    }
                    else
                    {
                        activePlayer.AddGenom(clickedtileColor, 1);
                        activePlayer.Grow(1);
                        Debug.Log("Genom Collected: " + clickedtileColor);
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
    public void ClickOnButton()
    {
        StartListening();
        if (CheckResources(1,0,0,0,0,0)&&!birdActive)
        {
            StarlingInstantiated = Instantiate(StarlingPrefab, activePlayer.Pos, Quaternion.identity);
            
            Cursor.visible = false; // Hide the cursor
            SetActiveBird(true);
        }
    }
    public void Update()
    {
        if (!birdActive) { return; }

        if (activePlayer.human&&thisListen)
        {
            
            if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape))
            {

                Confirm();
                SetActiveBird(false);
            }
            if (Input.GetMouseButtonDown(0))
            {


                Do(clickedTileObject, clickedtileColor);


            }
        }

    }
    public void Confirm()
    {

        SetActiveBird(false);

        if (StarlingInstantiated != null)
        {
            Destroy(StarlingInstantiated);
            StarlingInstantiated = null;
        }
        Cursor.visible = true;
        BirdDestroyed?.Invoke(activePlayer);
        StopListening();

    }






}
