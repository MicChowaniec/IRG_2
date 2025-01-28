using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class OnHoverScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private string label;
    private string description;

    private GameObjectTypeEnum GOTE;
    private ActionTypeEnum ATE;

    private bool button;
    private bool forStarling;

    public OnHoverSC onHoverSC;
    public Material litMaterial;

    public static event Action<string, string, bool, GameObjectTypeEnum, ActionTypeEnum> OnHoverBroadcast;
    public static event Action HidePopUp;

    private void OnEnable()
    {
        StarlingSkillScript.BirdActive += ForStarling;
    }

    private void OnDisable()
    {
        StarlingSkillScript.BirdActive -= ForStarling;

        // Prevent memory leaks
        HidePopUp = null;
        OnHoverBroadcast = null;
    }

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
        if (onHoverSC == null)
        {
            Debug.LogError("onHoverSC is not assigned.");
            return;
        }

        onHoverSC.AskForDetails();
        label = onHoverSC.label;
        description = forStarling ? onHoverSC.forStarlingText : onHoverSC.description;
        button = onHoverSC.button;
        GOTE = onHoverSC.GetChildObjectType();
        ATE = onHoverSC.GetChildObjectColor();

        OnHoverBroadcast?.Invoke(label, description, button, GOTE, ATE);
    }

    private void HandleHoverExit()
    {
        HidePopUp?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        HandleHoverEnter();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HandleHoverExit();
    }
}