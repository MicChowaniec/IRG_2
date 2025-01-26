using UnityEngine;
using System;
using Unity.VisualScripting;
using System.Runtime.CompilerServices;
using UnityEditor;

public class StarlingSkillScript : AbstractSkill
{


    public GameObject StarlingPrefab;
    private GameObject StarlingInstatiated;
    private bool birdActive;

    public static event Action<bool> BirdActive;
    public static event Action StarlingConsumed;
    public static event Action FishEaten;
    public static event Action SetNest;


    private GameObjectTypeEnum clickedTileObject;
    private ActionTypeEnum clickedtileColor;


    private void OnEnable()
    {
        
        BirdActive += SetActiveBird;
        PlayerManager.ActivePlayerBroadcast += ActivePlayerUpdate;

    }
    private void OnDisable()
    {
        OnHoverScript.OnHoverBroadcast -= CheckColorIncome;
        BirdActive -= SetActiveBird;
        PlayerManager.ActivePlayerBroadcast -= ActivePlayerUpdate;
    }

    private void ActivePlayerUpdate(int id)
    {
        PlayerManager pm = FindAnyObjectByType<PlayerManager>();
        activePlayer = pm.players[id];
    }

    private void CheckColorIncome(string arg1, string arg2, bool arg3, GameObjectTypeEnum @enum, ActionTypeEnum color)
    {
        clickedTileObject = @enum;
        Debug.Log("Tile Object: " + @enum);
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
    public override void Do(int range, bool self)
    {
        StatisticChange(-1, 0, 0, 0, 0, 0, 0);
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
                    Debug.Log("Genom Collected: " + clickedtileColor);
                    break;
                    //Add colors etc.
                }
            case GameObjectTypeEnum.Rock:
                {
                    SetNest?.Invoke();
                    Debug.Log("Nest Set");
                    break;
                }
            case GameObjectTypeEnum.Tree:
                {
                    break;
                }
            case GameObjectTypeEnum.Player:
                {
                    break;
                }


        }
        Confirm();
    }
    public void ClickOnButton()
    {
        if (CheckResources(1))
        {
            StarlingInstatiated = Instantiate(StarlingPrefab, activePlayer.Pos, Quaternion.identity);
            StarlingInstatiated.GetComponent<VisionSystem>().owner = activePlayer;
            Cursor.visible = false; // Hide the cursor
            BirdActive?.Invoke(true);
        }
    }
    public void Update()
    {
        if (!birdActive) { return; }
        else
        {
            if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape))
            {

                Confirm();
            }
            if (Input.GetMouseButtonDown(0))
            {


                Do(0, false);


            }
            
        }

    }
    public void Confirm()
    {
        StarlingInstatiated.GetComponent<VisionSystem>().owner = null;
        Destroy(StarlingInstatiated); StarlingInstatiated = null;
        BirdActive?.Invoke(false);
        Cursor.visible = true;
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
