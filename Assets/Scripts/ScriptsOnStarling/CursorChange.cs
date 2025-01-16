using UnityEngine;

public class RaycastCursorFollow : MonoBehaviour
{
   

    void Start()
    {
       

        if (mainCamera == null)
        {
            Debug.LogError("Main Camera is not assigned. Please assign it in the Inspector.");
        }
    }

   
}
