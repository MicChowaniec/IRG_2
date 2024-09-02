using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Turn", menuName = "Add Action", order = 3)]
public class Action : ScriptableObject
{
    public string Name;
    public string Description;
}
