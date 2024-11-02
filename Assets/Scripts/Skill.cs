using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Skill : MonoBehaviour, ISkill
{
    public GameAction GameAction;
    private GameObject player;
    public TurnBasedSystem tbs;

    public void Start()
    {
        tbs = FindAnyObjectByType<TurnBasedSystem>();
        player = tbs.activePlayer;
    }

    void PerformAction(bool movement, bool objectRequied, GameObjectTypeEnum obj1, GameObjectTypeEnum obj2, float range, bool targetSelf)
    {

        UpdateResources(GameAction.EnergyChange, GameAction.WaterChange, GameAction.BioMassChange, GameAction.StarlingChange, GameAction.DiseaseChange);

    }
    void UpdateResources(int energy, int water, int biomass, int starling, int disease)
    {
        PlayerScript ps = player.GetComponent<PlayerScript>();
        if (ps.energy + energy >= 0)
        {
            if (ps.water + water >= 0)
            {
                if (ps.bioMass + biomass >= 0)
                {
                    if (ps.starlings + starling >= 0)
                    {
                        ps.energy += energy;
                        ps.water += water;
                        ps.starlings += starling;
                        ps.bioMass += biomass;
                    }
                    else
                    {
                        PopUpPanel.RaisePopUpPanel("Not Enough Of Starlings");
                    }
                }
                else
                {
                    PopUpPanel.RaisePopUpPanel("Not Enough Of Biomass");
                }

            }
            else
            {
                PopUpPanel.RaisePopUpPanel("Not Enough Of Water");
            }





        }
        else
        {
            PopUpPanel.RaisePopUpPanel("Not Enough Of Energy");
        }
        tbs.diseaseLevel += disease;
    }
        void OnClick()
    {
        //NeedsToChange
        PerformAction(GameAction.Move,  GameAction.NeedsObject, GameAction.ObjectType1, GameAction.ObjectType2, GameAction.Range, GameAction.TargetSelf);
    }
}
