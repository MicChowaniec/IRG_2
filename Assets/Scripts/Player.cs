
using System.Collections.Generic;

using UnityEngine;


[CreateAssetMenu(fileName = "Player", menuName = "Add Player", order = 1)]
public class Player : Entity
{
    

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
        soulLvl = 1;
        if (itsName == "Purple")
        {
            eyes = 3;
        }
        else
        {
            eyes = 2;
        }
      
        PurpleLvl = 1;
        BlueLvl = 1;
        GreenLvl = 1;
        YellowLvl = 1;
        OrangeLvl =1;
        RedLvl = 1;

    }
    public int energy;
    public int soulLvl;
    public int eyes;
    public int burningLvl;

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
        if (protein < 0)
        {
            biomass -= protein;
            protein = 0;
        }
        CheckForDeath();
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
        CheckForDeath();
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



    public void WaterLoss(int waterLvl)
    {
        if (waterLvl>0&&water<=0)
        {
            biomass--;
            //implement fire;
        }
        else
        {
            water--;
            if (waterLvl > 0)
            {
                WaterLoss(waterLvl - 1);
            }
        }
        CheckForDeath();
    }
    public void Refill()
    {
        water = biomass;
    }


}
