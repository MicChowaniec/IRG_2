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
    public static event Action<int> StarlingConsumed;
    public static event Action<int> FishEaten;

    private GameObjectTypeEnum clickedTileObject;
    private Color clickedtileColor;


    private void OnEnable()
    {
        OnHoverScript.OnHoverBroadcast += CheckColorIncome;
        BirdActive += SetActiveBird;
    }
    private void OnDisable()
    {
        OnHoverScript.OnHoverBroadcast -= CheckColorIncome;
        BirdActive -= SetActiveBird;
    }

    private void CheckColorIncome(string arg1, string arg2, bool arg3, GameObjectTypeEnum @enum, Color color)
    {
       clickedTileObject = @enum;
       clickedtileColor = color;
    }

    private void SetActiveBird(bool b)
    {
        birdActive = b;
    }
    public override void Do(int range, bool self)
    {
        StatisticChange(-1, 0, 0, 0, 0, 0, 0);
        switch (clickedTileObject) 
        {
            case GameObjectTypeEnum.Water:
                {
                    activePlayer.Grow(1);
                    FishEaten(1);
                    break;
                }
            case GameObjectTypeEnum.Bush:
                {
                    break;
                    //Add colors etc.
                }
            
            
        }
    }
    public void ClickOnButton()
    {
        if (CheckResources(1))
        {
            StarlingInstatiated = Instantiate(StarlingPrefab, transform.position, Quaternion.identity);
            StarlingInstatiated.GetComponent<VisionSystem>().owner = activePlayer;
            Cursor.visible = false; // Hide the cursor
            BirdActive?.Invoke(true);
        }
    }
    public void Update()
    {
        if (!birdActive) { return; }
        else if (Input.GetMouseButtonDown(1)||Input.GetKeyDown(KeyCode.Escape))
            {
            StarlingInstatiated.GetComponent<VisionSystem>().owner = null;
            Destroy(StarlingInstatiated); StarlingInstatiated = null;
            BirdActive?.Invoke(false);
            Cursor.visible = true;
        }
        else if (Input.GetMouseButtonDown(0))
        {
           
            StarlingInstatiated.GetComponent<VisionSystem>().owner = null;
            Destroy(StarlingInstatiated); StarlingInstatiated = null;
            BirdActive?.Invoke(false);
            Do(0, false);
            Cursor.visible = true;

        }
    }

    public override void StatisticChange(int starling, int biomass, int water, int energy, int protein, int resistance, int eyes)
    {
        activePlayer.starlings += starling;
        StarlingConsumed?.Invoke(activePlayer.starlings);


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
