using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Player", menuName = "Add Player", order = 1)]
public class Player : ScriptableObject
{
    public int id;

    private void Awake()
    {
        Reset();
    }
    public void Reset()
    {
        Pos = StartPos;
        Rot = StartRot;
        water = 10;
        energy = 10;
        biomass = 10;
        protein = 10;
        starlings = 1;
        soulLvl = 1;
        
       
        if (id == 0)
        {
            eyes = 3;
        }
        else
        {
            eyes = 2;
        }
        PurpleLvl = 2;
        BlueLvl = 2;
        GreenLvl = 2;
        YellowLvl = 2;
        OrangeLvl =2;
        RedLvl = 2;

}
    public int water;
    public int energy;
    public int biomass;
    public int protein;
    public int starlings;
    public int soulLvl;
    public int eyes;
    public int burningLvl;


    public int PurpleLvl;
    public int BlueLvl;
    public int GreenLvl;
    public int YellowLvl;
    public int OrangeLvl;
    public int RedLvl;

    public int sequence;
    public string itsName;
    public string itsDescription;
    public Vector3 Pos, StartPos;
    public Quaternion Rot, StartRot;
    public bool human = false;
    public bool rooted = false;
    public GameObject Prefab;
    public GameObject TreePrefab;
    public Material material;

    public List<SkillScriptableObject> skills;
    public List<CardScriptableObject> cards;

    public void Grow(int Addedprotein)
    {
        Addedprotein *= GreenLvl;
        while (Addedprotein > 0)
        {
            if (protein <biomass)
            {
                protein++;
                Addedprotein--;
            }
            else if(protein>=biomass)
            {
                biomass++;
                Addedprotein--;
            }
        }

    }
    public void AddGenom(ActionTypeEnum ate, int amount)
    {
        switch (ate)
            {
            case ActionTypeEnum.Purple: PurpleLvl += amount;    break;
            case ActionTypeEnum.Blue:   BlueLvl += amount;      break;
            case ActionTypeEnum.Green:  GreenLvl += amount;     break;
            case ActionTypeEnum.Yellow: YellowLvl += amount;    break;
            case ActionTypeEnum.Orange: OrangeLvl += amount;    break;
            case ActionTypeEnum.Red:    RedLvl += amount;       break;
            case ActionTypeEnum.None:   break;
        }
    }
    public void ReceiveDamage(int amount)
    {
        float temp = Mathf.Sqrt((float)amount);
        temp -= RedLvl;
        if (temp > 0)
        {
            protein -= (int)temp;
        }
        else if (temp <= 0)
        {
            protein--;
        }
    }
    public int DealDamage()
    {
        System.Random rand = new();
        int temp = rand.Next(1, 21);
        temp += OrangeLvl;
        return temp;
    }
    public void Disease(int diseaseLvl)
    {
        //Implement Disease
        int temp = diseaseLvl - GreenLvl;
        if (temp > 0)
        {
            biomass -= temp;
        }
    }
    public void EnergyFromSun(int sunLvl)
    {
        if(energy+sunLvl>=biomass)
        {
            energy = biomass;
        }
        else 
        {
            energy+= sunLvl;
        }
    }

    public void StarlingUpdate()
    {
        starlings = soulLvl;
    }

    public void WaterUpdate(int waterLvl)
    {
        if (water - waterLvl <= biomass)
        {
            biomass--;
            //implement fire;
        }
        else
        {
            water-= waterLvl;
        }
    }

   
}
