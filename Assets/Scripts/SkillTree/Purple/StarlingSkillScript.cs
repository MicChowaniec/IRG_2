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

    
    public static event Action StarlingConsumed;
    public static event Action FishEaten;
    public static event Action<Player,Vector3,int> SetNest;
    public static event Action GenomChange;
    public static event Action<Player> BirdDestroyed;


    private GameObjectTypeEnum clickedTileObject;
    private ActionTypeEnum clickedtileColor;
    


    private void OnEnable()
    {
        
        
        PlayerManager.ActivePlayerBroadcast += ActivePlayerUpdate;
        OnHoverScript.OnHoverBroadcast += CheckColorIncome;

    }
    private void OnDisable()
    {
        OnHoverScript.OnHoverBroadcast -= CheckColorIncome;
        
        PlayerManager.ActivePlayerBroadcast -= ActivePlayerUpdate;
    }
 
    private void ActivePlayerUpdate(Player player)
    {
        activePlayer = player;
    }

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
        if (b)
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
                    FishEaten?.Invoke();
                    Debug.Log("Fish Eaten");

                    break;
                }
            case GameObjectTypeEnum.Bush:
                {
                    activePlayer.AddGenom(clickedtileColor, 1);
                    activePlayer.Grow(1);
                    GenomChange?.Invoke();
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
                        GenomChange?.Invoke();
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
        StatisticChange(-1, 0, 0, 0, 0, 0, 0);
        Confirm();
    }
    public void ClickOnButton()
    {
        if (CheckResources(1)&&!birdActive)
        {
            StarlingInstantiated = Instantiate(StarlingPrefab, activePlayer.Pos, Quaternion.identity);
            
            Cursor.visible = false; // Hide the cursor
            birdActive = true;
        }
    }
    public void Update()
    {
        if (!birdActive) { return; }

        if (activePlayer.human)
        {
            
            if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape))
            {

                Confirm();
                birdActive = false;
            }
            if (Input.GetMouseButtonDown(0))
            {


                Do(clickedTileObject, clickedtileColor);


            }
        }

    }
    public void Confirm()
    {

        birdActive = false;

        if (StarlingInstantiated != null)
        {
            Destroy(StarlingInstantiated);
            StarlingInstantiated = null;
        }
        Cursor.visible = true;
        BirdDestroyed?.Invoke(activePlayer);

    }

    public override void StatisticChange(int starling, int biomass, int water, int energy, int protein, int resistance, int eyes)
    {
        activePlayer.starlings += starling;
        if (activePlayer.human == true)
        {
            StarlingConsumed?.Invoke();
        }

    }

    public override bool CheckResources(int res)
    {
        if (activePlayer.starlings>=res)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    


}
