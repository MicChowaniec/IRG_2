using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Turn", menuName = "Add Turn", order = 2)]
public class Turn : ScriptableObject
{
    public Sprite icon;
    public string nameOfTurn;
    public string description;
    public List<Action> actions = new List<Action>();

    
}
