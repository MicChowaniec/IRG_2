using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PhotosynthesisSystem : MonoBehaviour
{
    public PlayerMovement pm;
    [SerializeField]
    public TurnBasedSystem tbs;
    [SerializeField]
    public GameObject notEnoughEnergy;
    [SerializeField]
    public GameObject notEnoughWater;
    public void ClickToGrow()
    {
        pm = tbs.activePlayer.GetComponent<PlayerMovement>();
        if (pm.CheckForEnergy(50))
        {
            if (pm.CheckForWater(50))
            {
                pm.UpdateEnergy(-50);
                pm.UpdateWater(-50);
                pm.bioMass += 100;
            }
            else
            {
                notEnoughWater.SetActive(true);
            }
        }
        else
        {
            notEnoughEnergy.SetActive(true);
        }
    }


}
