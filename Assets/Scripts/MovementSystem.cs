using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovementSystem : MonoBehaviour
{
    public bool movable = false;
    // Start is called before the first frame update
    public void OnButtonClick()
    {
        movable = true;
    }
}
