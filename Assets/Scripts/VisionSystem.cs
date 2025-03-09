using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;


public class VisionSystem : MonoBehaviour
{
    public LayerMask layer;


    public List<TileScriptableObject> ListOfDiscoveredFields = new();
    public Player owner;

    private PlayerManager _playerManager;
    private Collider[] _colliderBuffer = new Collider[10000]; // Adjust size as needed

    public static event Action<TileScriptableObject> FoundAttractiveField;

    private void OnEnable()
    {
        _playerManager = FindAnyObjectByType<PlayerManager>();

        StarlingSkillScript.SetNest += ScanForVisible;
        

        if (gameObject.activeInHierarchy)
        {
            ScanForVisible(owner, transform.position, owner.eyes);
        }
        PlayerManager.PlayersInstantiated += UpdateTheVision;
        PlayerManager.ActivePlayerBroadcast += UpdateTheVision;


    }
    public void Update()
    {
        UpdateTheVision();
    }
    private void UpdateTheVision()
    {
        ScanForVisible(owner, transform.position, owner.eyes);
    }
    private void UpdateTheVision(Player player)
    {
        if (player == owner)
        {
            ScanForVisible(player, transform.position, owner.eyes);
        }
    }

    private void OnDisable()
    {
  
        StarlingSkillScript.SetNest -= ScanForVisible;
        PlayerManager.PlayersInstantiated -= UpdateTheVision;


    }


    public void ScanForVisible(Player player, Vector3 center, int range)
    {
        if (player == owner)
        {


            int hitCount = Physics.OverlapSphereNonAlloc(center + new Vector3(0, 1, 0), range*0.7f, _colliderBuffer, layer);

            for (int i = 0; i < hitCount; i++)
            {
                GameObject go = _colliderBuffer[i].gameObject;
                CheckRecursively(go);

                if (go.TryGetComponent<TileScript>(out TileScript ts))
                {

                    if (!ListOfDiscoveredFields.Contains(ts.TSO))
                    {
                        ListOfDiscoveredFields.Add(ts.TSO);
                    }
                }
            }
        }
    }

    private void CheckRecursively(GameObject obj)
    {
        Queue<Transform> queue = new Queue<Transform>();
        queue.Enqueue(obj.transform);

        while (queue.Count > 0)
        {
            Transform current = queue.Dequeue();

            if (owner.human == true || (owner != null && owner.human == true))
            {
                current.gameObject.layer = 6;
            }

            foreach (Transform child in current)
            {
                queue.Enqueue(child);
            }
        }
    }
    public void FindAttractiveField(Player player, SkillScriptableObject sso)
    {

        if (player == owner)
        {
            List<TileScriptableObject> ListToWorkOn = new();

            //Dictionary<TileScriptableObject>
            foreach (var tso in ListOfDiscoveredFields)
            {
                if (tso.childType != GameObjectTypeEnum.None)
                {
                    ListToWorkOn.Add(tso);
                }
            }
            Debug.Log("Found " + ListToWorkOn.Count + " interesting fields");

            

            int numberOfWaterTiles = 0;
            int numberOfGrassTilesAdjancedToWater = 0;
            int numberOfGrassTilesAdjancedToWaterAndRock = 0;

            
                switch (sso.label)
                {
                    case "Move":
                    {
                        Debug.Log("Looking For palce to go");
                        foreach (TileScriptableObject tso in ListToWorkOn)
                        {
                            if (tso.childType == GameObjectTypeEnum.Water)
                            {
                                numberOfWaterTiles++;
                                foreach (var nei in tso.neighbours)
                                {
                                    if (nei.tileTypes == TileTypesEnum.Grass)
                                    {
                                        numberOfGrassTilesAdjancedToWater++;

                                        foreach (var rocky in nei.neighbours)
                                        {
                                            if (rocky.tileTypes == TileTypesEnum.Rock)
                                            {
                                                numberOfGrassTilesAdjancedToWaterAndRock++;
                                                if (nei.rootable)
                                                {
                                                    FoundAttractiveField?.Invoke(nei);
                                                    return;
                                                }

                                            }


                                        }
                                        if (nei.rootable)
                                        {
                                            FoundAttractiveField?.Invoke(nei);
                                        }

                                    }
                                }
                            }

                        }
                        break;
                    }
                    case "Starling":
                        {
                        Debug.Log("Looking for good place for my starling");
                        Dictionary<TileScriptableObject, int> valueOfTheTile = new();
                        TileScriptableObject returnedField = null;
                            int temp = 0;
                            if (ListToWorkOn.Count == 0)
                            {
                                FoundAttractiveField = null;
                            }
                            else
                            {
                            
                                foreach (TileScriptableObject prospect in ListToWorkOn)
                                {
                                    if (!valueOfTheTile.TryGetValue(prospect, out int value))
                                    {
                                        if (prospect.GetChildObjectType() == GameObjectTypeEnum.Rock)
                                        {

                                            valueOfTheTile.Add(prospect, CheckHowManyNeibourghsAreVisible(prospect));
                                            value = valueOfTheTile.GetValueOrDefault(prospect);
                                            if (value > temp)
                                            {
                                                returnedField = prospect;
                                                temp = valueOfTheTile[prospect];

                                            }
                                        }
                                            else if (prospect.GetChildObjectColor() == owner.GetPlayerType())
                                            {
                                                valueOfTheTile.Add(prospect, 3);
                                                returnedField = prospect;
                                            }
                                            else
                                            {
                                                valueOfTheTile.Add(prospect, 1);

                                            }
                                        }

                                    }



                                }
                                if (returnedField == null)
                                {
                                    returnedField = valueOfTheTile.FirstOrDefault().Key;
                                }
                                if (returnedField != null)
                                {
                                    FoundAttractiveField?.Invoke(returnedField);
                                }

                            }
                            break;
                        }
                }
            
        }
        
    
   
    public int CheckHowManyNeibourghsAreVisible(TileScriptableObject tso)
    {
                int fieldValue = 0;
            foreach (var neighbour in tso.neighbours)
            {

                if (!ListOfDiscoveredFields.Contains(neighbour))
                {
                fieldValue++;
                }
            }
            return fieldValue;
    }

 }



