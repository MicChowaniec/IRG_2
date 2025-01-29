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
    private Vector3 ScanningCenter;

    public static event Action<bool> BirdActive;
    public static event Action StarlingConsumed;
    public static event Action FishEaten;
    public static event Action<Vector3,int> SetNest;
    public static event Action GenomChange;


    private GameObjectTypeEnum clickedTileObject;
    private ActionTypeEnum clickedtileColor;
    


    private void OnEnable()
    {
        
        BirdActive += SetActiveBird;
        PlayerManager.ActivePlayerBroadcast += ActivePlayerUpdate;
        OnHoverScript.OnHoverBroadcast += CheckColorIncome;

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

    private void CheckColorIncome(string arg1, string arg2, Vector3 position, GameObjectTypeEnum type, ActionTypeEnum color)
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
    public override void Do(int range, bool self)
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
                    SetNest?.Invoke(ScanningCenter,3);

                    break;
                }
            case GameObjectTypeEnum.Tree:
                {
                    if (clickedtileColor != ActionTypeEnum.None)
                    {
                        SetNest?.Invoke(ScanningCenter,3);
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
        if (CheckResources(1))
        {
            StarlingInstatiated = Instantiate(StarlingPrefab, activePlayer.Pos, Quaternion.identity);

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
