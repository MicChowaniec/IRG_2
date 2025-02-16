using UnityEngine;

[CreateAssetMenu(fileName = "TreeSO", menuName = "Scriptable Objects/Dynamic/TreeSO")]
public class TreeSO : Entity
{ 
    public void ReceiveDamage(int amount)
    {
        biomass -= amount;
    }

    public void Disease(int diseaseLvl)
    {
        biomass = diseaseLvl;
    }

    public void WaterUpdate(int waterLvl)
    {
        if (water - waterLvl <0)
        {
            biomass -= waterLvl;
        }
        else if (water - waterLvl >=0 ) 
        { 
            water -= waterLvl;
        }
        
    }

    


}
