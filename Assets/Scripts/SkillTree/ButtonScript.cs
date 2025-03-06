using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public abstract class ButtonScript : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public SkillScriptableObject skill;
    public ActionManager ActionManager;
    protected Vector3 originalPosition;
    public GameObject holder;

    public static event Action<SkillScriptableObject> SkillListenerActivate;

    public void Start()
    {
       
        ActionManager = FindAnyObjectByType<ActionManager>();
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        Debug.Log("Calling " + skill.name);
        SkillListenerActivate?.Invoke(skill);
        AbstractSkill.DestroyTheButton += DestroyThis;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = transform.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData != null)
        {
            transform.position = originalPosition;
        }

    }
    public void OnDrag(PointerEventData eventData)
    {
        if (eventData != null)
        {
            transform.position = Input.mousePosition;
        }
    }
    public void DestroyThis()
    {
        AbstractSkill.DestroyTheButton -= DestroyThis;
        Destroy(holder);
        Debug.Log("Object Destroyed");
    }
}
