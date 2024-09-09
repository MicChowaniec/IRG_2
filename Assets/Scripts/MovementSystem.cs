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
    public void OnButtonClick()
    {
        if (tbs.activePlayer.GetComponent<PlayerMovement>().rooted == false)
        {
            movable = true;
        }
        else
        {
            cannotMoveRooted.SetActive(true);
        }
    }
    public void CloseCannotMoveRooted()
    {
        cannotMoveRooted.SetActive(false);
    }

}
