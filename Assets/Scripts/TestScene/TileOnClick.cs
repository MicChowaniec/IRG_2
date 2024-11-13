using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileOnClick : MonoBehaviour
{
    public void OnMouseDown()
    {
        GetComponentInChildren<TileOnClick>().enabled = true;
    }
}
