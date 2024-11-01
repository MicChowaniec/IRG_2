using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class WaterSystem : MonoBehaviour
{

    public TurnBasedSystem tbs;
    public PlayerScript ps;
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
        ps = tbs.activePlayer.GetComponent<PlayerScript>();
        ts = mm.tiles[ps.tileIdLocation];
        if (ps.rooted == true)
        {
            if (ps.CheckForEnergy(10))
            {
                ps.UpdateWater((GrassFieldCount() + WaterFieldCount()) * 6);
                ps.UpdateEnergy(-10);
            }
            else
            {
                notEnoughEnergy.SetActive(true);
            }

        }
        else
        {
            if (ps.CheckForEnergy(10))
            {

                if (WaterFieldCount() > 0)
                {
                    ps.UpdateWater(50);
                    ps.UpdateEnergy(-10);
                }
            }
            else
            {
                notEnoughEnergy.SetActive(true);
            }
        }
            
    }
}
