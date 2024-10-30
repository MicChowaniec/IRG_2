using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionSystem : MonoBehaviour
{
    public LayerMask layer;
    public float range;
    // Start is called before the first frame update
    void Start()
    {
        ScanForVisible(range);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ScanForVisible(float range)
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, range, layer);

        foreach (Collider hit in hits)
        {
            GameObject go = hit.gameObject;

            go.layer = 6;
        }
    }
}
