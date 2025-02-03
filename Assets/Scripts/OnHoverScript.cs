using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms.GameCenter;
using UnityEditor.Search;

public class OnHoverScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private string label;
    private string description;

    private GameObjectTypeEnum GOTE;
    private ActionTypeEnum ATE;

    private bool forStarling;

    public OnHoverSC onHoverSC;
    public Material litMaterial;

    public static event Action<string, string, Vector3, GameObjectTypeEnum, ActionTypeEnum> OnHoverBroadcast;
    public static event Action HidePopUp;
    public static event Action<string, string> ShowSkillDescription;
    public static event Action HideSkillDescription;


    private void ForStarling(bool isActive)
    {
        forStarling = isActive;
    }

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
            Debug.LogError("onHoverSC is not assigned.");
            return;
        }

        onHoverSC.AskForDetails();
        label = onHoverSC.label;
        description = forStarling ? onHoverSC.forStarlingText : onHoverSC.description;

        GOTE = onHoverSC.GetChildObjectType();
        
        ATE = onHoverSC.GetChildObjectColor();

        OnHoverBroadcast?.Invoke(label, description, transform.position, GOTE, ATE);
    }

    private void HandleHoverExit()
    {
        StopHighlight();
        HidePopUp?.Invoke();
    }

    public void OnMouseEnter()
    {
        Debug.Log("Pointer Hover");
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
        label = onHoverSC.label;
        description = forStarling ? onHoverSC.forStarlingText : onHoverSC.description;
        ShowSkillDescription(label, description);
    }


}