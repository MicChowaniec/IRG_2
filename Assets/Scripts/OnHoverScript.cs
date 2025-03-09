using UnityEngine;
using System;
using UnityEngine.EventSystems;


public class OnHoverScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public OnHoverSC onHoverSC;
    public Material litMaterial;

    public static event Action<TileScriptableObject> OnHoverBroadcast;
    public static event Action HidePopUp;
    public static event Action<SkillScriptableObject> ShowSkillDescription;
    public static event Action HideSkillDescription;


    public void Highlight()
    {
        foreach (Transform child in this.transform)
        {
            if (child.TryGetComponent<MeshRenderer>(out MeshRenderer meshRenderer))
                {
                var materials = meshRenderer.materials;
                Array.Resize(ref materials, materials.Length + 1);
                materials[^1] = litMaterial;

                meshRenderer.materials = materials;
            }
        }
    }



        public void StopHighlight()
    {
        foreach (Transform child in this.transform)
        {

            if (child.TryGetComponent<MeshRenderer>(out MeshRenderer meshRenderer))
            {
                var materials = meshRenderer.materials;
                while (materials.Length > 1)
                {
                    Array.Resize(ref materials, materials.Length - 1);
                    meshRenderer.materials = materials;
                }
            }


        }
    }

    private void HandleHoverEnter()
    {

        Highlight();
        if (onHoverSC == null)
        {
            Debug.Log("OnHoverSC Null as FUCK");
            return;
        }
        Debug.Log("ThisShouldBeInvoked");
        if (onHoverSC is TileScriptableObject)
        {
            Debug.Log("This is TileScriptableObject");
            TileScriptableObject tileScriptableObject = (TileScriptableObject)onHoverSC;
            OnHoverBroadcast?.Invoke(tileScriptableObject);
        }
  
    }

    private void HandleHoverExit()
    {
        StopHighlight();
        HidePopUp?.Invoke();
    }

    public void OnMouseEnter()
    {
        Debug.Log("Now It Should be displayed by OnMouseEnter");
        HandleHoverEnter();
    }

    public void OnMouseExit()
    {
        HandleHoverExit();
    }

    public void OnPointerExit(PointerEventData eventData)
    {

        HideSkillDescription?.Invoke() ;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (onHoverSC == null)
        {
            Debug.Log("OnHoverSC Null as FUCK");
            return;
        }
        Debug.Log("ThisShouldBeInvoked");
        if (onHoverSC is SkillScriptableObject )
        {

            Debug.Log("This is SkillScriptableObject");
            SkillScriptableObject skillScriptableObject = (SkillScriptableObject)onHoverSC;
            
            ShowSkillDescription?.Invoke(skillScriptableObject);
        }

    }

}