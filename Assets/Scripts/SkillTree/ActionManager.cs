
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
   
    public List<GameObject> purpleSkills = new();
    public List<GameObject> blueSkills = new();
    public List<GameObject> greenSkills = new();
    public List<GameObject> yellowSkills = new();
    public List<GameObject> orangeSkills = new();
    public List<GameObject> redSkills = new();
    public List<GameObject> basicSkills = new();
    private List<List<GameObject>> AllSkills => new ()
    {
        purpleSkills,
        blueSkills,
        greenSkills,
        yellowSkills,
        orangeSkills,
        redSkills,
        basicSkills
    };

    private Turn activeTurn;
    public static event Action SkillsUpdated;

    private void OnEnable()
    {

        ButtonScript.SkillListenerActivate += ActivateSkillListener;
        AbstractSkill.AnimationObjectDestroyed += ResetListeners;
        DragOnScript.CallTheAction += ActivateSkillListener;


    }
    private void OnDisable()
    { 
        ButtonScript.SkillListenerActivate -= ActivateSkillListener;
        AbstractSkill.AnimationObjectDestroyed -= ResetListeners;
        DragOnScript.CallTheAction += ActivateSkillListener;
    }


    private void ResetListeners(Player player)
    {
        foreach (List<GameObject> list in AllSkills)
        { 
            foreach (GameObject obj in list)
                {
                    obj.SetActive(false);
                }
        }
    }
    private void ActivateSkillListener(SkillScriptableObject skillSO)
    {
        Debug.Log("Heard you " + skillSO.name);
        foreach (List<GameObject> list in AllSkills)
        {
            
            foreach (GameObject obj in list)
            {

                obj.TryGetComponent<AbstractSkill>(out var abstractSkill);
                if (abstractSkill.skill == skillSO)
                {
                    Debug.Log("Found " + obj.name);
                    if (!obj.activeSelf)
                    {
                        Debug.Log("Activated " + obj.name);
                        obj.SetActive(true);

                        return;

                    }

                }
            }
        }
        Debug.Log("...But didn't found");
    }


}
