using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class WaterSystem : MonoBehaviour
{
    [SerializeField]
    public TurnBasedSystem tbs;
    public PlayerMovement pm;
    public TileScript ts;
    [SerializeField]
    public MapManager mm;


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
            pm.UpdateWater((GrassFieldCount()+WaterFieldCount())*6);
        }
        else
        {
            pm.UpdateWater(WaterFieldCount()*6);
        }
    }
}
