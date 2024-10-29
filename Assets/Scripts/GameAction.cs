using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Action", menuName = "Add Action", order = 3)]
public class GameAction : ScriptableObject
{
    public string Name;
    public string Description;
    public ActionTypeEnum Type;
    public Sprite Sprite;

    public float Range;
    public int EnergyChange;
    public int WaterChange;
    public bool NeedsWaterTile;
    public bool NeedsBush;
    public int BioMassChange;
    public bool Move;
    public bool TargetSelf;
    public bool FireChange;
    public bool StarlingChange;
    public bool DestroyTarget;
    public bool SingleTarget;
    public int DiseaseChange;
    public int Damage;

}
