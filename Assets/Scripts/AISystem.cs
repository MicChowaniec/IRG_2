using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISystem : MonoBehaviour
{
    public TurnBasedSystem tbs;
    public MapManager mm;
    private GameObject activePlayer;

    
    public void AIBehaviour(ActionTypeEnum ate)
    {

        if (activePlayer.GetComponent<PlayerScript>().human == false)
        {


            activePlayer = tbs.activePlayer;

           


        }
    }
    
}
