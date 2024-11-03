using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Action", menuName = "Add Action", order = 3)]
public class GameAction : ScriptableObject
{
    
    public string Name;
    public string Description;
    public ActionTypeEnum Type1;
    public ActionTypeEnum Type2;
    public Sprite Sprite;

    public float Range;
    public int EnergyChange;
    public int WaterChange;
    public bool NeedsObject;
    public GameObjectTypeEnum ObjectType1;
    public GameObjectTypeEnum ObjectType2;
    public int BioMassChange;
    public bool Move;
    public bool TargetSelf;
    public int FireChange;
    public int StarlingChange;
    public bool DestroyTarget;
    public bool SingleTarget;
    public int DiseaseChange;
    public int Damage;
    public void OnClick()
    {
        //Add Event Listener
    }

}
