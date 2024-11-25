using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;

public class OnClickTile :MonoBehaviour
{
    public static event Action<Vector3> OnClick;

    private void OnMouseDown()
    {
        
        OnClick?.Invoke(this.transform.position);
    }


} 

