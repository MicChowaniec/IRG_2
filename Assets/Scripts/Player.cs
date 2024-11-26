using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Add Player", order = 1)]
public class Player : ScriptableObject
{
    public int sequence;
    public string itsName;
    public string itsDescription;
    public Vector3 startPos;
    public Quaternion startRot = Quaternion.identity;
    public bool human = false;
    public GameObject Prefab;
    public GameObject TreePrefab;
    public Material material;

}
