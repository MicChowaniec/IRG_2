using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using System;


public class DragOnScript : MonoBehaviour, IDropHandler
{
    public SkillScriptableObject skill;
    public static event Action<SkillScriptableObject> CallTheAction;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            Destroy(eventData.pointerDrag);
            CallTheAction?.Invoke(skill);

        }
    }
}
