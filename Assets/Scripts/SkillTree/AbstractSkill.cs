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
    public bool thisListen = false;
    public GameObjectTypeEnum clickedTileObject;
    public ActionTypeEnum clickedtileColor;
    public void StatisticChange(int starling, int biomass,  int water, int protein, int energy, int eyes)

    {
        activePlayer.starlings += starling;
        activePlayer.biomass += biomass;
        activePlayer.water += water;
        activePlayer.protein += protein;
        activePlayer.energy += energy;
        activePlayer.eyes += eyes;
        if(activePlayer.human)
        {
            ChangeResourcesStatus?.Invoke();
        }
    }
    public virtual void Do(GameObjectTypeEnum gote, ActionTypeEnum ate)

    {

    }
    public bool CheckResources(int starling, int biomass, int water, int protein, int energy, int eyes)
    {
        bool temp =true;
        if (starling >0)
        {
            if (activePlayer.starlings >= starling)
            {
                temp = temp && true;
            }
            else
            {
                temp = temp && false;
            }


        }
        if (biomass > 0)
        {
            if (activePlayer.biomass >= biomass)
            {
                temp = temp && true;
            }
            else
            {
                temp = temp && false;
            }
        }
        if (water > 0)
        {
            if (activePlayer.water >= water)
            {
                temp = temp && true;
            }
            else
            {
                temp = temp && false;
            }

        }
        if (protein > 0)
        {
            if (activePlayer.protein >= protein)
            {
                temp = temp && true;
            }
            else
            {
                temp = temp && false;
            }
        }
        if (energy > 0)
        {
            if (activePlayer.energy >= energy)
            {
                temp = temp && true;
            }
            else
            {
                temp = temp && false;
            }
        }
        if (eyes > 0)
        {
            if (activePlayer.eyes >= eyes)
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
    public void OnEnable()
    {


        PlayerManager.ActivePlayerBroadcast += ActivePlayerUpdate;


    }
    public void OnDisable()
    {


        PlayerManager.ActivePlayerBroadcast += ActivePlayerUpdate;


    }


    public void ActivePlayerUpdate(Player player)
    {
        player = activePlayer;
    }

    public void ChangeGenom(ActionTypeEnum ate)
    {
        if (activePlayer.human)
        {
            activePlayer.AddGenom(ate,1);
            GenomChange?.Invoke();
        }
    }

    public void StartListening()
    {
        thisListen = true;
    }
    public void StopListening()
    {
        thisListen=false;
    }




}
