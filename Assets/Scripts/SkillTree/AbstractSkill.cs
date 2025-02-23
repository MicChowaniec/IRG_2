using JetBrains.Annotations;
using UnityEngine;
using System;
using UnityEditor;

public  class AbstractSkill : MonoBehaviour
{
    public SkillScriptableObject skill;
    protected Player activePlayer;
    public static event Action Change;
    public OnHoverSC tso;









    public void CheckColorIncome(OnHoverSC onHoverSC)
    {
        tso = onHoverSC;
    }

    public void StatisticChange()

    {
        activePlayer.starlings -= skill.starling;
        activePlayer.biomass -= skill.biomass;
        activePlayer.water -= skill.water;
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
    public virtual void Do(OnHoverSC tso)
    {

    }
    public virtual void Do()

    {

    }
    public void OnEnable()
    {
        AI.ExecuteSelf += Do;
        AI.Execute += Do;
    PlayerManager.ActivePlayerBroadcast += ActivePlayerUpdate;
    OnHoverScript.OnHoverBroadcast += CheckColorIncome;


}
public void OnDisable()
    {
        AI.ExecuteSelf -= Do;
        AI.Execute -= Do;
        PlayerManager.ActivePlayerBroadcast -= ActivePlayerUpdate;
        OnHoverScript.OnHoverBroadcast -= CheckColorIncome;

    }
    public bool CheckResources()


    {
        bool temp =true;
        if (skill.starling>0)
        {
            if (activePlayer.starlings >= skill.starling)
            {
                temp = temp && true;
            }
            else
            {
                temp = temp && false;
            }


        }
        if (skill.biomass > 0)
        {
            if (activePlayer.biomass >= skill.biomass)
            {
                temp = temp && true;
            }
            else
            {
                temp = temp && false;
            }
        }
        if (activePlayer.water > 0)
        {
            if (activePlayer.water >= skill.water)
            {
                temp = temp && true;
            }
            else
            {
                temp = temp && false;
            }

        }
        if (activePlayer.protein > 0)
        {
            if (activePlayer.protein >=skill.protein)
            {
                temp = temp && true;
            }
            else
            {
                temp = temp && false;
            }
        }
        if (activePlayer.energy > 0)
        {
            if (activePlayer.energy >= skill.energy)
            {
                temp = temp && true;
            }
            else
            {
                temp = temp && false;
            }
        }
        if (activePlayer.eyes > 0)
        {
            if (activePlayer.eyes >= skill.eyes)
            {
                temp = temp && true;
            }
            else
            {
                temp = temp && false;
            }
        }
        return temp;
    }
   


    public void ActivePlayerUpdate(Player player)
    {
        activePlayer = player;
    }

    public void ChangeGenom(ActionTypeEnum ate)
    {

            activePlayer.AddGenom(ate,1);
            Change?.Invoke();
 
    }
    public void Update()
    {


        if (activePlayer.human)
        {

            if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape))
            {

                Confirm();
        
            }
            if (Input.GetMouseButtonDown(0))
            {

                if (skill.self)
                {
                    Do();
                }
                else
                {
                    Do(tso);
                }

            }
        }

    }
    public virtual void Confirm()
    {

    }
    public virtual void ClickOnButton()
    {

    }


}
