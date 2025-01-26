using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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
        
        fireResistance = -10;
        toxicResistance = 0;
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
    public int fireResistance;
    public int toxicResistance;

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

    public List<CardScriptableObject> cards;

    public void Grow(int Addedprotein)
    {
        Addedprotein = Addedprotein * GreenLvl;
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
            case ActionTypeEnum.None: break;
        }
    }
    

}
