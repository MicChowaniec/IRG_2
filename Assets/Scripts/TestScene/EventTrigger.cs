using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class EventTrigger : MonoBehaviour, IPointerClickHandler
{
    public static event Action Skill;

    public void OnPointerClick(PointerEventData eventData)
    {
        Skill?.Invoke();
        Debug.Log("Clicked");
    }

}
