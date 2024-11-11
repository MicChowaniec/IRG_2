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
            List<GameAction> possibilities = new List<GameAction>();

            activePlayer = tbs.activePlayer;

            foreach (GameAction ga in activePlayer.GetComponent<PlayerScript>().actions)
            {
                if (ga.Type1 == ate)
                {
                    //Add type2
                    possibilities.Add(ga);

                }
            }
            possibilities[UnityEngine.Random.Range(0, possibilities.Count - 1)].Execute();


        }
    }
    
}
