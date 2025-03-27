using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
using UnityEditor;
using System.IO;
using UnityEditor.Experimental.GraphView;
using UnityEditor.TerrainTools;
using UnityEngine.Rendering;


public class PlayerScript : MonoBehaviour
{

    public Player player;
    public GameObject playerGameObject;
    public Animator animator;
    public TileScriptableObject tile;
    private int sunLvl;
    private int diseaseLvl;
    public static event Action<Player> AITurn;
    private Coroutine movementCoroutine;
    public PlayerManager pm;
    public string GameLog;
    public Vector3 startingPosition;

    public static event Action RootAction;
    public void OnEnable()
    {
        GameLog = "";
        //PlayerManager.ActivePlayerBroadcast
        PlayerManager.ActivePlayerBroadcast += SetActivePlayer;
        SunLevel.DayEvent += SunLvl;
        PlayerManager.ChangePhase += ChangePhaseChanges;
        pm = FindAnyObjectByType<PlayerManager>();
        SunLevel.NightEvent += DestroyThis;
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
        SunLevel.NightEvent -= DestroyThis;
    }

    private void ChangePhaseChanges()
    {
        player.Disease(diseaseLvl);
        player.EnergyFromSun(sunLvl);
    }

    private void SetActivePlayer(Player invokedPlayer)
    {

        if (invokedPlayer == player)
        {
            StartCoroutine(ChooseAMoveCoroutine());
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
            if (player.water < 5)
            {
                foreach (var nei in tile.neighbours)
                {
                    if (nei.childType == GameObjectTypeEnum.Water)
                    {
                        player.Refill();
                        GameLog += "003,";
                        FinishThisTurn();
                        return;
                    }
                }
            }
            else if (tile.rootable&&player.water>=5)
            {
                Vector3 rememberLocalScale = playerGameObject.transform.localScale;
                GameObject tree = Instantiate(player.TreePrefab, playerGameObject.transform.position, Quaternion.identity, tile.representation.transform);
                tile.childType = GameObjectTypeEnum.Tree;
                tree.transform.localScale = rememberLocalScale / 5;

                tile.rootable = false;
                tile.passable = false;
                tile.SetOwner(player);
                player.water -= 5;
                if (tile.tileTypes == TileTypesEnum.Grass)
                {
                    foreach (var tile in tile.neighbours)
                    {
                        tile.rootable = false;
                        tile.SetOwner(player);

                    }
                }
                RootAction?.Invoke();
                GameLog += "002,";
                FinishThisTurn();
                return;
            }
            
            
                List<TileScriptableObject> tileWherePlayerCouldGo = new();
                List<TileScriptableObject> enemies = new();
                foreach (var nei in tile.neighbours)
                {
                    if (nei.passable)
                    {
                        tileWherePlayerCouldGo.Add(nei);

                    }
                    if(nei.stander!=null)
                    {
                        enemies.Add(nei);
                    }
                }
                System.Random rand = new System.Random();

                if (tileWherePlayerCouldGo.Count > 0)
                {
                    TileScriptableObject tileTemp = tileWherePlayerCouldGo[rand.Next(tileWherePlayerCouldGo.Count)];
                    GameLog += "001,";
                    MoveTo(tileTemp.coordinates,5);
                }
                else if (enemies.Count> 0)
            {
                PushTheEnemy(enemies[rand.Next(tileWherePlayerCouldGo.Count)]);
                
            }
            else
                {
                    GameLog += "000,";
                    FinishThisTurn();
                    return;
                
            }
        }
    }
    private void PushTheEnemy(TileScriptableObject tileScriptableObject)
    {
        GameObject enemy = pm.GetGameObjectFromSO(tileScriptableObject.stander);
        Vector3 myPoisition = this.transform.position;
        Vector3 enemyPosiion = enemy.transform.position;
        Vector3 PushPosition = 2 * enemyPosiion - myPoisition;
        enemy.GetComponent<PlayerScript>().MoveTo(PushPosition, 10);
        GameLog += "004,";

    }
    private void FinishThisTurn()
    {
        StartCoroutine(DelayedFinishTurn());
    }

    private IEnumerator DelayedFinishTurn()
    {
        yield return new WaitForSeconds(0.1f);
        pm.ChangePlayer();
    }
    public void MoveTo(Vector3 newDestination,float speed)
    {
        startingPosition = playerGameObject.transform.position;
        if (movementCoroutine != null)
        {
            StopCoroutine(movementCoroutine);
        }
        movementCoroutine = StartCoroutine(SmoothlyMoveToDestination(newDestination, speed));
    }
    public void MoveBack()
    {
        
        if (movementCoroutine != null)
        {
            StopCoroutine(movementCoroutine);
        }
        movementCoroutine = StartCoroutine(SmoothlyMoveToDestination(startingPosition, 10));
    }

    private IEnumerator SmoothlyMoveToDestination(Vector3 destination, float speed)
    {

        Transform transformOfPlayer = playerGameObject.transform;
        transformOfPlayer.LookAt(destination, Vector3.up);
        playerGameObject.GetComponent<Animator>().SetTrigger("Move");

        while (Vector3.Distance(transformOfPlayer.position, destination) > 0.1f)
        {
            transformOfPlayer.position = Vector3.MoveTowards(transformOfPlayer.position, destination, Time.deltaTime*speed);
            yield return null;
        }
        playerGameObject.GetComponent<Animator>().SetTrigger("Idle");
        transformOfPlayer.position = destination;
        movementCoroutine = null;
        FinishThisTurn();
    }

    private IEnumerator ChooseAMoveCoroutine()
    {
        yield return new WaitUntil(() => tile != null);
        MakeAction(player.human);
    }
    private void DestroyThis()
    {
        GameLog += player.ownedFields+ "\n";
        string path = Path.Combine(Application.persistentDataPath, $"{player.itsName}StepWithAttack.txt");
        File.AppendAllText(path, GameLog);
        Destroy(playerGameObject);
    }
}



