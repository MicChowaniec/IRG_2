using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
using UnityEditor;


public class PlayerScript : MonoBehaviour
{

    public Player player;
    public Animator animator;
    public TileScriptableObject tile;
    private int sunLvl;
    private int diseaseLvl;
    public static event Action FinishTurn;
    public static event Action<Player> AITurn;

    public void OnEnable()
    {
        //PlayerManager.ActivePlayerBroadcast
        PlayerManager.ActivePlayerBroadcast += SetActivePlayer;
        SunLevel.DayEvent += SunLvl;
        PlayerManager.ChangePhase += ChangePhaseChanges;
    }
    public void SunLvl(int lvl)
    {
        sunLvl = lvl;
    }

    public void OnDisable()
    {
        PlayerManager.ActivePlayerBroadcast -= SetActivePlayer;
        PlayerManager.ChangePhase -= ChangePhaseChanges;
        SunLevel.DayEvent -= SunLvl;
    }

    private void ChangePhaseChanges()
    {
        player.Disease(diseaseLvl);
        player.EnergyFromSun(sunLvl);
        player.WaterLoss(1);
        

    }

    private void SetActivePlayer(Player invokedPlayer)
    {

        if (invokedPlayer == player)
        {
            MakeAction(player.human);
        }
        else
        {
            return;
        }
    }
    // Start is called before the first frame update
    public void Start()
    {
        
        transform.position = player.StartPos;
        this.transform.rotation = player.StartRot;
        animator = GetComponent<Animator>();
        animator.SetTrigger("Idle");
        if (player.human==true)
        {
            StrategyCameraControl scc = FindAnyObjectByType<StrategyCameraControl>();
            scc.objectToCenterOn = transform;
        }
    }
  


    public void UpdatePosition()
    {
        player.Pos = transform.position;
        player.Rot = transform.rotation;
    }
    private void MakeAction(bool human)
    {
        if (!human)
        {
            AITurn?.Invoke(player);

        }
    }


}
