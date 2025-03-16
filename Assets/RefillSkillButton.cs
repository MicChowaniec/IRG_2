using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class RefillSkillButton : ButtonScript
{
   

   
    public void Update()
    {
        
        if (activePlayer.human)
        {
            if (!CheckResources())
            {
                this.GetComponent<Button>().interactable = false;
            }
            else if (CheckForWater())
                {
                    this.GetComponent<Button>().interactable = true;
                }
            
        }
        
    }
    public bool CheckForWater()
    {
        
        foreach (var tile in tileWherePlayerStands.neighbours)
        {
            if(tile.gote== GameObjectTypeEnum.Water)
            {
                return true;
            }
            
        }
        return false;
    }

}
