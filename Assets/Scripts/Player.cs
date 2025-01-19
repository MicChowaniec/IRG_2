using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Add Player", order = 1)]
public class Player : ScriptableObject
{
    public int id;

    private void Awake()
    {
        Reset();
    }
    private void Reset()
    {
        Pos = StartPos;
        Rot = StartRot;
        water = 12;
        energy = 12;
        biomass = 12;
        protein = 12;
        starlings = 1;
        maxStarlings = 1;
        eyes = 2;
        fireResistance = -10;
        toxicResistance = 0;

    }
    public int water;
    public int energy;
    public int biomass;
    public int protein;
    public int starlings;
    public int maxStarlings;
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
        while (protein<=biomass)
        {
            protein++;
            Addedprotein--;
        }
        if (protein == biomass)
        {
            while (Addedprotein + protein > biomass)
            {
                biomass++;
                Addedprotein--;
                if (Addedprotein + protein > biomass)
                {
                    protein++;
                    Addedprotein--;
                }


            }
        }

    }

}
