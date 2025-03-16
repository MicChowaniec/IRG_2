using System;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Entity", menuName = "Scriptable Objects/BaseClasses/Entity")]
public class Entity : ScriptableObject, IDamagable
{
    public int id;

    public static event Action<int> GrowTheTree;
    public static event Action<string> Death;

    public int biomass;
    public int protein;
    public int water;


    public int PurpleLvl;
    public int BlueLvl;
    public int GreenLvl;
    public int YellowLvl;
    public int OrangeLvl;
    public int RedLvl;

    protected ActionTypeEnum Type;

    protected TileScriptableObject tile;


    public void Grow(int Addedprotein)
    {
        int _biomass = biomass;
        Addedprotein *= GreenLvl;
        while (Addedprotein > 0)
        {
            if (protein < biomass)
            {
                protein++;
                Addedprotein--;
            }
            else if (protein >= biomass)
            {
                biomass++;
                Addedprotein--;
            }
        }
        GrowTheTree?.Invoke(_biomass-biomass);
    }
    public void CheckForDeath()
    {
        if (biomass <= 0)
        {
            Death?.Invoke("You died");
        }
    }
    public ActionTypeEnum GetPlayerType()
    {
        return Type;
    }
}
    
