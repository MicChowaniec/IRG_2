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
    private int rootingPressure;
    public GameSettings gameSeetings;
    public Player computerPlayer;
    public GameObject computerPlayerObject;
    public SkillScriptableObject rootSkill;
    public SkillScriptableObject moveSkill;
    public SkillScriptableObject photoSkill;
    public SkillScriptableObject waterSkill;

    public TileScriptableObject tileScripttemp;


    public static event Action EndTurn;
    public static event Action<Player, SkillScriptableObject> SendMeAField;
    public static event Action<SkillScriptableObject> PickASkill;
    public static event Action Prepare;
    public static event Action<TileScriptableObject> Execute;
    public static event Action ExecuteSelf;
    public static event Action AddNeighbours;

    public bool allFieldsNeighbourized;


    private void OnEnable()
    {
        allFieldsNeighbourized = false;
        TurnBasedSystem.CurrentTurnBroadcast += UpdateTurn;
        PlayerManager.ActivePlayerBroadcast += UpdatePlayer;
        AbstractSkill.AnimationObjectDestroyed += SkipTurn;
        AbstractSkill.AIButtonClicked += PrepareAction;

    }


    private void OnDisable()
    {
        TurnBasedSystem.CurrentTurnBroadcast -= UpdateTurn;
        PlayerManager.ActivePlayerBroadcast -= UpdatePlayer;
        AbstractSkill.AnimationObjectDestroyed -= SkipTurn;
        AbstractSkill.AIButtonClicked -= PrepareAction;
    }

    public void UpdatePlayer(Player player)
    {
        if (player != computerPlayer)
        {
            computerPlayer = player;
            if (!computerPlayer.human)
            {
                computerPlayerObject = playerManager.GetGameObjectFromSO(computerPlayer);
                StartCoroutine(CalculateMoveCoroutine(player));
            }



        }

    }

    private void FindPossibleMoves(Player computer)
    {
        SkillList.Clear();
        if (computer.human)
        {
            return;
        }

        foreach (var card in computer.cards)
        {
            if (!computer.rooted)
            {
                SkillList.Add(card.skillNotRootedSC);

            }
            else if (computer.rooted)
            {
                SkillList.Add(card.skillRootedSC);
            }
        }
        SkillList.Add(ActiveTurn.SpecialSkill);
    }
    private IEnumerator CalculateMoveCoroutine(Player player)
    {
        yield return new WaitForSeconds(1);
        CalculateMove(player);

    }
    private void CalculateMove(Player computer)
    {

        Debug.Log("Started Calculating Move");
        FindPossibleMoves(computer);
        if (computer.human)
        {
            Debug.Log("Sorry, human turn.");
            return;
        }
        else
        {
            Debug.Log("Ok, I am computer");
            if (computerPlayerObject)
            {
                Debug.Log("I have my Object");
                if (computerPlayerObject.TryGetComponent<PlayerScript>(out PlayerScript ps))
                {

                    if (ps != null)
                    {
                        tileScripttemp = ps.tile;
                        if (tileScripttemp.rootable)
                        {
                            Debug.Log("Also found if I am standing on rootable");
                            if (SkillList.Contains(rootSkill))
                            {
                                tempSkill = rootSkill;
                                PickASkill?.Invoke(rootSkill);

                                return;
                            }
                        }
                        else
                        {
                            if (computer.water < 5)
                            {
                                
                                foreach (var tileO in tileScripttemp.neighbours)
                                {
                                    if (tileO.gote == GameObjectTypeEnum.Water)
                                    {
                                        tempSkill = waterSkill;
                                        PickASkill?.Invoke(waterSkill);

                                        return;
                                    }
                                }
                            }
                            tempSkill = photoSkill;
                            PickASkill?.Invoke(photoSkill);

                            return;
                        }
                    }

                    else
                    {
                        Debug.Log("I found that I am not standing on rootable");

                        foreach (var field in computerPlayerObject.GetComponent<PlayerScript>().tile.neighbours)
                        {
                            if (field.stander == null && field.passable)
                            {
                                tileScripttemp = field;
                                tempSkill = moveSkill;
                                PickASkill?.Invoke(moveSkill);

                                return;
                            }
                        }

                    }
                }

            }
              
            else
            {
                Debug.Log("I don't see my object");
                computerPlayerObject = playerManager.GetGameObjectFromSO(computer);
                CalculateMove(computer);
            }
        }
    }

    IEnumerator WaitForNSeconds(int seconds)
    {
        yield return new WaitForSeconds((float)seconds); // Waits for N seconds
    }

    public void PrepareAction()
    {
        Debug.Log("PreparingTheAction");
        if (!computerPlayer.human)
        {
            Debug.Log("Looks Like The Player Is Not Human");

            Prepare?.Invoke();

            if (tempSkill.self)
            {

                ExecuteSelf?.Invoke();
                Debug.Log("Executed SelfSkill");
            }
            else if (!tempSkill.self)
            {

                Execute?.Invoke(tileScripttemp);
                Debug.Log("Executed SkillOnOtherField");
            }
        }
    }

    private void UpdateTurn(Turn turn)
    {
        ActiveTurn = turn;
        rootingPressure++;

    }

    private void SkipTurn()
    {
        if (!computerPlayer.human)
        {

            tempSkill = null;
            EndTurn?.Invoke();
        }
    }
    private void SkipTurn(Player player)
    {
        if (!computerPlayer.human)
        {
            tempSkill = null;
            EndTurn?.Invoke();
        }
    }





}
