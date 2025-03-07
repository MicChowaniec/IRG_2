using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

public class AI : MonoBehaviour
{
    public Turn ActiveTurn;
    public List<SkillScriptableObject> SkillList = new();
    public SkillScriptableObject skipTurn;
    public ActionManager actionManager;
    public PlayerManager playerManager;
    private SkillScriptableObject tempSkill;
    public Player computerPlayer;
    public GameObject computerPlayerObject;
    public SkillScriptableObject rootSkill;
    public SkillScriptableObject moveSkill;
    public SkillScriptableObject photoSkill;
    public TileScriptableObject tileScripttemp;

    public static event Action EndTurn;
    public static event Action<SkillScriptableObject> PickASkill;
    public static event Action Prepare;
    public static event Action<TileScriptableObject> Execute;
    public static event Action ExecuteSelf;

    private float actionTimer = 0f;
    private const float actionTimeout = 10f;
    private bool actionTaken = false;

    private void OnEnable()
    {
        TurnBasedSystem.CurrentTurnBroadcast += UpdateTurn;
        PlayerManager.ActivePlayerBroadcast += UpdatePlayer;
        PlayerScript.AITurn += WaitForFields;
        AbstractSkill.AIButtonClicked += PrepareAction;
    }

    private void OnDisable()
    {
        TurnBasedSystem.CurrentTurnBroadcast -= UpdateTurn;
        PlayerManager.ActivePlayerBroadcast -= UpdatePlayer;
        PlayerScript.AITurn -= WaitForFields;
        AbstractSkill.AIButtonClicked -= PrepareAction;
    }

    private void Update()
    {
        if (!computerPlayer.human && !actionTaken)
        {
            actionTimer += Time.deltaTime;
            if (actionTimer >= actionTimeout)
            {
                Debug.Log("AI took too long. Skipping turn.");
                SkipTurn();
            }
        }
    }

    public void UpdatePlayer(Player player)
    {
        if (player != computerPlayer)
        {
            computerPlayer = player;
        }
    }

    private void WaitForFields(Player player)
    {
        computerPlayer = player;
        CalculateMove(computerPlayer);
    }

    private void CalculateMove(Player computer)
    {
        if (computer.human) return;
        actionTimer = 0f;
        actionTaken = false;

        if (computerPlayerObject)
        {
            if (computerPlayerObject.TryGetComponent<PlayerScript>(out PlayerScript ps))
            {
                if (ps.tile.rootable && SkillList.Contains(rootSkill))
                {
                    tempSkill = rootSkill;
                    PickASkill?.Invoke(rootSkill);
                }
                else
                {
                    tempSkill = moveSkill;
                    PickASkill?.Invoke(moveSkill);
                }
                actionTaken = true;
            }
        }
        else
        {
            computerPlayerObject = playerManager.GetGameObjectFromSO(computer);
            CalculateMove(computer);
        }
    }

    public void PrepareAction()
    {
        if (!computerPlayer.human)
        {
            Prepare?.Invoke();
            actionTimer = 0f;
            actionTaken = true;

            if (tempSkill.self)
            {
                ExecuteSelf?.Invoke();
            }
            else
            {
                Execute?.Invoke(tileScripttemp);
            }
        }
    }

    private void UpdateTurn(Turn turn)
    {
        ActiveTurn = turn;
        actionTimer = 0f;
        actionTaken = false;
    }

    private void SkipTurn()
    {
        if (!computerPlayer.human)
        {
            tempSkill = null;
            EndTurn?.Invoke();
            actionTimer = 0f;
            actionTaken = true;
        }
    }
}
