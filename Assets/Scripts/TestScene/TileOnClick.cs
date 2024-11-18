using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileOnClick : MonoBehaviour
{
    public void OnMouseDown()
    {
        
        Transform childTransform = transform.Find("Canvas"); 
        if (childTransform != null)
        {
            childTransform.gameObject.SetActive(true);
        }
    }
}
