using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms.GameCenter;
using UnityEditor.Search;

public class OnHoverScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public OnHoverSC onHoverSC;
    public Material litMaterial;

    public static event Action<OnHoverSC> OnHoverBroadcast;
    public static event Action HidePopUp;
    public static event Action<string, string> ShowSkillDescription;
    public static event Action HideSkillDescription;


    public void Highlight()
    {
        var meshRenderer = GetComponent<MeshRenderer>();
        var materials = meshRenderer.materials;

        Array.Resize(ref materials, materials.Length + 1);
        materials[^1] = litMaterial;

        meshRenderer.materials = materials;
    }

    public void StopHighlight()
    {
        var meshRenderer = GetComponent<MeshRenderer>();
        var materials = meshRenderer.materials;

        if (materials.Length > 1)
        {
            Array.Resize(ref materials, materials.Length - 1);
            meshRenderer.materials = materials;
        }
    }

    private void HandleHoverEnter()
    {
        Highlight();
        if (onHoverSC == null)
        {
           
            return;
        }

        OnHoverBroadcast?.Invoke(onHoverSC);
  
    }

    private void HandleHoverExit()
    {
        StopHighlight();
        HidePopUp?.Invoke();
    }

    public void OnMouseEnter()
    {

        HandleHoverEnter();
    }

    public void OnMouseExit()
    {
        HandleHoverExit();
    }

    public void OnPointerExit(PointerEventData eventData)
    {

        HideSkillDescription();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
        onHoverSC.AskForDetails();
       
    }


}