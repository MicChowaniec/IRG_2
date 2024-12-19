using Unity.Services.Core;
using UnityEngine;

public class TreeGrowing : MonoBehaviour
{

    public Vector3 scale;
    [SerializeField]
    float size;
    public void Start()
    {

        scale = transform.localScale;

    }
    public void OnSizeChange(float size)
    {
        this.transform.localScale = scale * size;
    }
    public void Update()
    {
        OnSizeChange(size);
    }

   

}
