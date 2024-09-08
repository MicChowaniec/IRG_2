using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class WaterSystem : MonoBehaviour
{

    public TurnBasedSystem tbs;
    public PlayerMovement pm;
    public TileScript ts;
    public MapManager mm;
    public GameObject notEnoughEnergy;



    public int WaterFieldCount()
    {
        int temp = 0;
        foreach(int id in ts.neighbours)
        {
            if (mm.tiles[id].tileType.type == TileTypesEnum.Water)
            {
                temp++;
            }
        }
        return temp;
    }
    public int GrassFieldCount()
    {
        int temp = 0;
        foreach (int id in ts.neighbours)
        {
            if (mm.tiles[id].tileType.type == TileTypesEnum.Water)
            {
                temp++;
            }
        }
        return temp;
    }
    public void ButtonClickDrink()
    {
        pm = tbs.activePlayer.GetComponent<PlayerMovement>();
        ts = mm.tiles[pm.tileIdLocation];
        if (pm.rooted == true)
        {
            if (pm.CheckForEnergy(10))
            {
                pm.UpdateWater((GrassFieldCount() + WaterFieldCount()) * 6);
                pm.UpdateEnergy(-10);
            }
            else
            {
                notEnoughEnergy.SetActive(true);
            }

        }
        else
        {
            if (pm.CheckForEnergy(10))
            {

                if (WaterFieldCount() > 0)
                {
                    pm.UpdateWater(50);
                    pm.UpdateEnergy(-10);
                }
            }
            else
            {
                notEnoughEnergy.SetActive(true);
            }
        }
            
    }
}
