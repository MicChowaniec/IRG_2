using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public abstract class ButtonScript : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public SkillScriptableObject skill;
    public ActionManager actionManager;
    protected Vector3 originalPosition;
    public GameObject holder;
    public PlayerManager playerManager;
    public Player activePlayer;

    public static event Action<SkillScriptableObject> SkillListenerActivate;

    public void Start()
    {
        playerManager = FindAnyObjectByType<PlayerManager>(); 
        actionManager = FindAnyObjectByType<ActionManager>();
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
        activePlayer = playerManager.activePlayer;

    }
    public void Update()
    {
        if(activePlayer.human)
        {
            if(!CheckResources())
            {
                this.GetComponent<Button>().interactable = false;
            }
        }
        
    }
    public bool CheckResources()

    {
        bool temp = true;

        if (activePlayer.biomass > 0)
        {
            if (activePlayer.biomass >= skill.biomass)
            {
                temp = true;
            }
            else
            {
                return false;
            }
        }
        if (activePlayer.water > 0)
        {
            if (activePlayer.water >= skill.water)
            {
                temp = true;
            }
            else
            {
                return false;
            }

        }
        if (activePlayer.protein > 0)
        {
            if (activePlayer.protein >= skill.protein)
            {
                temp = true;
            }
            else
            {
                return false;
            }
        }
        if (activePlayer.energy > 0)
        {
            if (activePlayer.energy >= skill.energy)
            {
                temp = true;
            }
            else
            {
                return false;
            }
        }
        if (activePlayer.eyes > 0)
        {
            if (activePlayer.eyes >= skill.eyes)
            {
                temp = true;
            }
            else
            {
                return false;
            }
        }
        return temp;
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
