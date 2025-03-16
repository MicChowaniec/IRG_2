
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    public PlayerManager playerManager;
    public Player activePlayer;
    public PlayerScript playerScript;
    public GameObject playerGameObject;
    public TileScriptableObject tilewherePlayerStands;


    public List<GameObject> AllSkills = new();
  

    private Turn activeTurn;
    public static event Action SkillsUpdated;



    private void OnEnable()
    {
        TurnBasedSystem.CurrentTurnBroadcast += UpdateTurn;
        ButtonScript.SkillListenerActivate += ActivateSkillListener;
        PlayerManager.ActivePlayerBroadcast += PlayerChange;
        DragOnScript.CallTheAction += ActivateSkillListener;
        AbstractSkill.DestroyTheButton += ResetListeners;




    }
    private void OnDisable()
    {
        TurnBasedSystem.CurrentTurnBroadcast -= UpdateTurn;
        ButtonScript.SkillListenerActivate -= ActivateSkillListener;
        PlayerManager.ActivePlayerBroadcast -= PlayerChange;
        DragOnScript.CallTheAction -= ActivateSkillListener;
        AbstractSkill.DestroyTheButton -= ResetListeners;

    }

    private void UpdateTurn(Turn turn)
    {
        if(turn!=activeTurn)
        {
            activeTurn = turn;
        }
    }

    private void PlayerChange(Player player)
    {

        if (!player.human)
        {
            return;
        }
        else
        {
            activePlayer = player;
            playerGameObject = playerManager.GetGameObjectFromSO(player);
            playerScript = playerGameObject.GetComponent<PlayerScript>();
            InvokeRepeating(nameof(CheckTile), 0f, 0.1f); // Sprawdzaj co 0.1 sekundy
        }
    }

        void CheckTile()
        {

            if (playerScript.tile != null)
            {
                tilewherePlayerStands = playerScript.tile;
                Debug.Log("Tile zosta³o przypisane!");

                // Wy³¹czamy wywo³ywanie tej metody
                CancelInvoke(nameof(CheckTile));
            }
        }
    




  

    private void ResetListeners(Player player)
    {

            foreach (GameObject obj in AllSkills)
            {
            if (obj.activeSelf)
            {
                obj.GetComponent<AbstractSkill>().Confirm();

                obj.SetActive(false);
            }
            }
        
    }
    private void ResetListeners()
    {
        foreach (GameObject obj in AllSkills)
        {
            if (obj.activeSelf)
            {
                obj.GetComponent<AbstractSkill>().Confirm();


                obj.SetActive(false);
            }
         
        }
    }
    private void ActivateSkillListener(SkillScriptableObject skillSO)
    {
        ResetListeners();
        Debug.Log("Heard you " + skillSO.name);


            foreach (GameObject obj in AllSkills)
            {
               
                if (obj.TryGetComponent<AbstractSkill>(out var abstractSkill))
                {
                    if (abstractSkill.skill == skillSO)
                    {
                        Debug.Log("Found " + obj.name);
                        if (!obj.activeSelf)
                        {
                            
                            Debug.Log("Activated " + obj.name);
                            obj.SetActive(true);
                            abstractSkill.ActivePlayerUpdate(activePlayer);
                            abstractSkill.ActivePlayerObjectUpdate(playerGameObject);
                            tilewherePlayerStands= playerGameObject.GetComponent<PlayerScript>().tile;
                            abstractSkill.TileWherePlayerStandsUpdate(tilewherePlayerStands);
                            return;

                        }
                    }

                }
            }
        
        Debug.Log("...But didn't found");
    }
    IEnumerator WaitOneSecond()
    {

        yield return new WaitForSeconds(1f); // Waits for 1 second

    }


}
