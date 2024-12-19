using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class VisionSystem : MonoBehaviour
{
    public LayerMask layer;
    public float range;
    private int[] ListOfDiscoveredFields;
  

    public static event Action<int[]> AddVisibleFields;

     public void ScanForVisible()
    {
        //Consider using OverlapSpehereNonAlloc
        Collider[] hits = Physics.OverlapSphere(transform.position, range, layer);

        foreach (Collider hit in hits)
        {
            GameObject go = hit.gameObject;
            CheckRecursively(go);
        }
        AddVisibleFields?.Invoke(ListOfDiscoveredFields);
    }

    private void CheckRecursively(GameObject obj)
    {
        

        foreach (Transform child in obj.transform)
        {
            CheckRecursively(child.gameObject);
        }
    }
}

