using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor.Rendering;
using System.Linq;
using Unity.VisualScripting;

public class VisionSystem : MonoBehaviour
{
    public LayerMask layer;


    public List<TileScriptableObject> ListOfDiscoveredFields = new();
    public bool human;
    public Player owner;

    private PlayerManager _playerManager;
    private Collider[] _colliderBuffer = new Collider[100]; // Adjust size as needed

    private void OnEnable()
    {
        _playerManager = FindAnyObjectByType<PlayerManager>();

        StarlingSkillScript.SetNest += ScanForVisible;

        if (gameObject.activeInHierarchy)
        {
            ScanForVisible(transform.position,owner.eyes);
        }
    }

    private void OnDisable()
    {

        StarlingSkillScript.SetNest -= ScanForVisible;
    }

    public void ScanForVisible(Vector3 center,int range)
    {
        if (_playerManager == null) return;

        if (TryGetComponent<PlayerScript>(out PlayerScript playerScript))
        {
            human = playerScript.player.human;
        }
        else if (owner != null)
        {
            human = owner.human;
        }


        int hitCount = Physics.OverlapSphereNonAlloc(center+new Vector3(0,1,0),range*0.7f, _colliderBuffer, layer);

        for (int i = 0; i < hitCount; i++)
        {
            GameObject go = _colliderBuffer[i].gameObject;
            CheckRecursively(go);

            if (go.TryGetComponent<TileScript>(out TileScript ts))
            {
                
                if (ListOfDiscoveredFields.Contains(ts.TSO))
                {
                    if (owner != null)
                    {
                        _playerManager.GetGameObjectFromSO(owner)
                            ?.GetComponent<VisionSystem>()
                            ?.ListOfDiscoveredFields.Add(ts.TSO);
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

            if (human == true || (owner != null && owner.human == true))
            {
                current.gameObject.layer = 6;
            }

            foreach (Transform child in current)
            {
                queue.Enqueue(child);
            }
        }
    }
    public TileScriptableObject FindAttractiveField(string skill, GameObjectTypeEnum gote, ActionTypeEnum ate)
    {


        List<TileScriptableObject> ListToWorkOn = new();

        //Dictionary<TileScriptableObject>
        foreach (var tso in ListOfDiscoveredFields)
        {
            if (tso.childType == gote)
            {
                if (tso.childColor == ate)
                {
                    ListToWorkOn.Add(tso);
                }
            }
        }
        switch (skill)
        {
            case "Starling":

                if (gote == GameObjectTypeEnum.Rock)
                {
                    Dictionary<TileScriptableObject, int> valueOfTheTile = new();
                    foreach (var tso in ListToWorkOn)
                    {
                        int fieldValue = 6;
                        foreach (var neighbour in tso.neighbours)
                        {
                            if (ListOfDiscoveredFields.Contains(neighbour))
                            {
                                fieldValue--;
                            }
                        }
                        valueOfTheTile.Add(tso, fieldValue);


                    }
                    var returnedField = valueOfTheTile.FirstOrDefault().Key;
                    foreach (var prospect in valueOfTheTile.Keys)
                    {
                        if (valueOfTheTile[prospect] > valueOfTheTile[returnedField])
                        {
                            returnedField = prospect;
                        }
                    }
                    return returnedField;

                }
                else if(gote==GameObjectTypeEnum.Bush)
                {
                    foreach(var tso in ListOfDiscoveredFields)
                    {
                        if (tso.childColor== ate)
                        {
                            return tso;
                        }
                    }
                    return null;
                }
                else if (gote==GameObjectTypeEnum.Water)
                {
                    foreach (var tso in ListToWorkOn)
                    {
                        if (tso.childType==gote)
                        {
                            return tso;
                        }
                    }
                }   return null; 
            }
        return null;
    }
    public ActionTypeEnum AskForColor()
    {
        foreach (var v in ListOfDiscoveredFields)
        {
            if(v.childColor!=ActionTypeEnum.None)
            {
                return v.childColor;
            }
            //Implement Wages
        }
        return ActionTypeEnum.None;
    }

 }



