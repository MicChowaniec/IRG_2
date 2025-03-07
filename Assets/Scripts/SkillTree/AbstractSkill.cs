
using UnityEngine;
using System;

using System.Collections;

public  class AbstractSkill : MonoBehaviour
{
    public GameObject animationObject;
    public GameObject animationObjectInstantiated;
    public SkillScriptableObject skill;
    public Player activePlayer;
    public static event Action Change;
    public TileScriptableObject tso;
    public VisionSystem visionSystem;

    public bool ButtonClicked;

    public static event Action<Player> AnimationObjectDestroyed;
    public static event Action WhoIsTheActivePlayer;
    public static event Action DestroyTheButton;
    public static event Action AIButtonClicked;

    public void OnEnable()
    {
        ButtonClicked = false;
        AI.Prepare += ClickOnButton;
        AI.ExecuteSelf += Do;
        AI.Execute += Do;
        
        PlayerManager.ActivePlayerBroadcast += ActivePlayerUpdate;
        OnHoverScript.OnHoverBroadcast += CheckColorIncome;

    }
    public void OnDisable()

    {
        AI.Prepare -= ClickOnButton;
        AI.ExecuteSelf -= Do;
        AI.Execute -= Do;

        PlayerManager.ActivePlayerBroadcast -= ActivePlayerUpdate;
        OnHoverScript.OnHoverBroadcast -= CheckColorIncome;
    }

    
    /// <summary>
    /// Changing value of public value OnHoverSC tso by incoming onHoverSC Scriptable Object which is send by hovering the tile
    /// </summary>
    /// <param name="onHoverSC"></param>
    public void CheckColorIncome(TileScriptableObject tileScriptableObject)
    {
        tso = tileScriptableObject;
    }

    public void StatisticChange()

    {
        activePlayer.biomass -= skill.biomass;
        activePlayer.WaterLoss(skill.water);
        activePlayer.protein -= skill.protein;
        activePlayer.energy -= skill.energy;
        activePlayer.eyes -= skill.eyes;
        activePlayer.AddGenom(skill.actionType, 1);
        Debug.Log(skill.actionType + " used. +1 to " + skill.actionType+ " genom");
        if (activePlayer.human)
        {
            Change?.Invoke();
        }
    }



    
    /// <summary>
    /// Cheching whether player resources are higher or equal than skill resource
    /// </summary>
    /// <returns>true if resources are higher or equal than skill demands, false if there is at least one resource which is lower than skill demand</returns>
    public bool CheckResources()

    {
        bool temp = true;
        
        if (activePlayer.biomass > 0)
        {
            if (activePlayer.biomass >= skill.biomass)
            {
                temp = true;
            }
            else
            {
                return false;
            }
        }
        if (activePlayer.water > 0)
        {
            if (activePlayer.water >= skill.water)
            {
                temp = true;
            }
            else
            {
                return false;
            }

        }
        if (activePlayer.protein > 0)
        {
            if (activePlayer.protein >=skill.protein)
            {
                temp = true;
            }
            else
            {
                return false;
            }
        }
        if (activePlayer.energy > 0)
        {
            if (activePlayer.energy >= skill.energy)
            {
                temp = true;
            }
            else
            {
                return false;
            }
        }
        if (activePlayer.eyes > 0)
        {
            if (activePlayer.eyes >= skill.eyes)
            {
                temp = true;
            }
            else
            {
                return false;
            }
        }
        return temp;
    }
   

    /// <summary>
    /// Update activePlayer, and vision System
    /// </summary>
    /// <param name="player"></param>
    public void ActivePlayerUpdate(Player player)
    {
        activePlayer = player;
        ClickOnButton();
    }

    /// <summary>
    /// Increase by one genom Lvl of ActionTypeEnum equivalent to  skill just played
    /// </summary>
    /// <param name="ate"></param>
    public void ChangeGenom(ActionTypeEnum ate)
    {
            activePlayer.AddGenom(ate,1);
            Change?.Invoke();
    }

    /// <summary>
    /// Update for human player, right mouse or esc to call Confirm(), left mouse to call Do().
    /// </summary>
    public void Update()
    {
        
            if (activePlayer)
            {
                if (activePlayer.human)
                {

                    if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape))
                    {

                        Confirm();

                    }
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (CheckResources())
                        {
                            if (skill.self)
                        {

                            Do();
                            StartCoroutine(WaitForNSeconds(2));

                        }
                        else
                        {
                            Do(tso);
                            StartCoroutine(WaitForNSeconds(2));

                        }





                    }
                        else if(!CheckResources())
                        {
                        //DisplayPopUp;
                        }
                    
                    }
                }
            }
            else
            {
                WhoIsTheActivePlayer?.Invoke();
            }
        
    }

    IEnumerator WaitForNSeconds(int seconds)
    {

        yield return new WaitForSeconds((float)seconds); // Waits for 1 second

    }
    /// <summary>
    /// Start the animation of skill
    /// </summary>
    /// 

    public void ClickOnButton()
    {
        if (CheckResources()&&!ButtonClicked)
        {

            ButtonClicked = true;
            if (animationObject != null)
            {
                animationObjectInstantiated = Instantiate(animationObject, activePlayer.Pos, Quaternion.identity);
                if (activePlayer.human)
                {
                    Cursor.visible = false; // Hide the cursor
                }
            }
            AIButtonClicked?.Invoke();

        }
    }
    /// <summary>
    /// Ending animation of skill
    /// </summary>
    public void Confirm()
    {
        ButtonClicked = false;
        if (animationObjectInstantiated != null)
        {
            Destroy(animationObjectInstantiated);
            animationObjectInstantiated = null;
            
        }
        Cursor.visible = true;
    }
    public void DisableFunction()
    {
        DestroyTheButton?.Invoke();
        AnimationObjectDestroyed?.Invoke(activePlayer);
    }

    /// <summary>
    /// Function to ovverride, must consist Confirm() if it is done
    /// </summary>
    /// <param name="tso"></param>
    public virtual void Do(TileScriptableObject tso)
    {

    }
    public virtual void Do()
    {

    }
   
}
