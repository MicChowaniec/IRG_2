using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkill
{
    
    void PerformAction(bool movement, bool objectRequied, GameObjectTypeEnum obj1, GameObjectTypeEnum obj2, float range, bool TargetSelf) 
    { 

    }
    void UpdateResources(int energy, int water, int biomass, int starling, int disease )
    { 

    }
    void OnClick()
    { 

    }
    
}
