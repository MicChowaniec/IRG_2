using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class VisionSystem : MonoBehaviour
{
    public LayerMask layer;
    public float range;
    public List<int> ListOfDiscoveredFields = new();
    public bool human;
    public Player owner;

    public void Start()
    {
        
        if (TryGetComponent<PlayerScript>(out PlayerScript playerScript) == true)
        {
            human = playerScript.player.human;
        }
        ScanForVisible();

    }

    public void ScanForVisible()
    {
        //Consider using OverlapSpehereNonAlloc
        Collider[] hits = Physics.OverlapSphere(transform.position, range, layer);

        foreach (Collider hit in hits)
        {
            GameObject go = hit.gameObject;
            CheckRecursively(go);
            if (go.TryGetComponent<TileScript>(out TileScript ts)==true)
            {
                int idToAdd = ts.TSO.id;
                ListOfDiscoveredFields.Add(idToAdd);
                if (owner != null)
                {
                    owner.GetComponent<VisionSystem>().ListOfDiscoveredFields.Add(idToAdd);
                }
            }

            
            if (human ==true)
            {
                go.layer = 6;
            }
          
        }
    }

    private void CheckRecursively(GameObject obj)
    {
        

        foreach (Transform child in obj.transform)
        {
            CheckRecursively(child.gameObject);
        }
    }
}

