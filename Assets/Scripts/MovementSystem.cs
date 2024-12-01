using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;

public class MovementSystem : MonoBehaviour
{
    public bool movable = false;
    [SerializeField]
    public TurnBasedSystem tbs;
    [SerializeField]
    public GameObject cannotMoveRooted;
    // Start is called before the first frame update
   
  

}
