using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceAtIcon : MonoBehaviour
{
    
    public Transform theObserver;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
       this.transform.LookAt(theObserver);
    }
}
