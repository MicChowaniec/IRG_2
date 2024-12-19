using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Add Player", order = 1)]
public class Player : ScriptableObject
{
    private void Awake()
    {
        Reset();
    }
    private void Reset()
    {
        Pos = StartPos;
        Rot = StartRot;
    }
    public int sequence;
    public string itsName;
    public string itsDescription;
    public Vector3 Pos, StartPos;
    public Quaternion Rot, StartRot;
    public bool human = false;
    public GameObject Prefab;
    public GameObject TreePrefab;
    public Material material;

}
