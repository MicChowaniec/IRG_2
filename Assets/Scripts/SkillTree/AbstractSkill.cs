using JetBrains.Annotations;
using UnityEngine;
using System;
using UnityEditor;

public abstract class AbstractSkill : MonoBehaviour
{
    public SkillScriptableObject skill;
    public Player activePlayer;
    public static event Action ChangeResourcesStatus;
    public static event Action GenomChange;

    public bool thisListener;
    public OnHoverSC tso;

    public void ThisListener(bool b)
    {
        thisListener = b;
        if (b && activePlayer.human)
        {
            OnHoverScript.OnHoverBroadcast += CheckColorIncome;
        }
        if (!b)
        {
            OnHoverScript.OnHoverBroadcast -= CheckColorIncome;
        }
    }
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
        if(activePlayer.human)
        {
            ChangeResourcesStatus?.Invoke();
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


        PlayerManager.ActivePlayerBroadcast += ActivePlayerUpdate;


    }
    public void OnDisable()
    {


        PlayerManager.ActivePlayerBroadcast += ActivePlayerUpdate;


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
            GenomChange?.Invoke();
 
    }
    public void Update()
    {
        if (!thisListener) { return; }

        if (activePlayer.human && thisListener)
        {

            if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape))
            {

                Confirm();
                ThisListener(false);
            }
            if (Input.GetMouseButtonDown(0))
            {


                Do(tso);


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
