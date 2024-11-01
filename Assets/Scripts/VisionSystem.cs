using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionSystem : MonoBehaviour
{
    public LayerMask layer;
    public LayerMask finalLayer;
    public float range;
    public PlayerScript ps;

    void Start()
    {
        if (ps.picked==true)
        {
            ScanForVisible();
        }
        
    }

    void Update()
    {

    }

    public void ScanForVisible()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, range, layer);

        foreach (Collider hit in hits)
        {
            GameObject go = hit.gameObject;
            SetLayerRecursively(go, 6);
        }
    }

    private void SetLayerRecursively(GameObject obj, int newLayer)
    {
        obj.layer = newLayer;

        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }
}

