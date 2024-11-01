using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PhotosynthesisSystem : MonoBehaviour
{
    public PlayerScript ps;
    [SerializeField]
    public TurnBasedSystem tbs;
    [SerializeField]
    public GameObject notEnoughEnergy;
    [SerializeField]
    public GameObject notEnoughWater;
    public void ClickToGrow()
    {
        ps = tbs.activePlayer.GetComponent<PlayerScript>();
        if (ps.CheckForEnergy(50))
        {
            if (ps.CheckForWater(50))
            {
                tbs.activePlayer.GetComponent<Rigidbody>().AddForce(0, 1, 0,ForceMode.Impulse);
                ps.UpdateEnergy(-50);
                ps.UpdateWater(-50);
                ps.UpdateBioMass(100);
                tbs.activePlayer.transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
                
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
