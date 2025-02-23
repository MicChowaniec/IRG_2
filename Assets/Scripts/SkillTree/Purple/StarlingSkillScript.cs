using UnityEngine;
using System;
using Unity.VisualScripting;
using System.Runtime.CompilerServices;
using UnityEditor;

public class StarlingSkillScript : AbstractSkill
{


    public GameObject StarlingPrefab;
    public GameObject StarlingInstantiated;


    
    public static event Action<Player,Vector3,int> SetNest;
    public static event Action<Player> BirdDestroyed;
    public static event Action<Player> UpdateVision;

    


    public override void Do(OnHoverSC tso)
    {
        Debug.Log("Starting Doing Skill");
        if (tso is TileScriptableObject)
        {
            GameObjectTypeEnum gote = tso.GetChildObjectType();
            ActionTypeEnum ate = tso.GetChildObjectColor();
            Vector3 scanningCenter = tso.GetPosition();
            Debug.Log(gote + ", " + ate);
            switch (gote)
            {
                case GameObjectTypeEnum.Water:
                    {
                        activePlayer.Grow(2);

                        Debug.Log("Fish Eaten");

                        break;
                    }
                case GameObjectTypeEnum.Bush:
                    {

                        activePlayer.AddGenom(ate, 1);
                        activePlayer.Grow(1);
                        Debug.Log("Genom Collected: " + ate);

                        break;

                    }
                case GameObjectTypeEnum.Rock:
                    {
                        SetNest?.Invoke(activePlayer, scanningCenter, 3);
                        Debug.Log("Nest Set");
                        break;
                    }
                case GameObjectTypeEnum.Tree:
                    {
                        if (tso.GetChildObjectColor() != ActionTypeEnum.None)
                        {
                            SetNest?.Invoke(activePlayer, scanningCenter, 3);
                        }
                        else
                        {
                            activePlayer.AddGenom(ate, 1);
                            activePlayer.Grow(1);
                            Debug.Log("Genom Collected: " + ate);
                        }
                        break;
                    }
                case GameObjectTypeEnum.Player:
                    {
                        Player target = tso.GetStander();
                        if (target != activePlayer)
                        {
                            if (target.eyes >= 1)
                            {
                                target.eyes--;
                                Debug.Log("Player: " + ate.ToString()+" blinded.");
                            }

                        }
                        else
                        {
                            target.eyes++;
                            UpdateVision?.Invoke(activePlayer);
                            Debug.Log("You have got an additional eye");

                        }
                        break;


                    }
                case GameObjectTypeEnum.None:
                    {
                        Debug.Log("That tile was empty!");                   
                            return;

                    }


            }
            StatisticChange();
            Confirm();
        }
    }
    public override void ClickOnButton()
    {
       
        if (CheckResources())
        {
            StarlingInstantiated = Instantiate(StarlingPrefab, activePlayer.Pos, Quaternion.identity);
            
            Cursor.visible = false; // Hide the cursor
            
        }
    }
   
    public override void Confirm()
    {



        if (StarlingInstantiated != null)
        {
            Destroy(StarlingInstantiated);
            StarlingInstantiated = null;
        }
        Cursor.visible = true;
        BirdDestroyed?.Invoke(activePlayer);
        


    }






}
