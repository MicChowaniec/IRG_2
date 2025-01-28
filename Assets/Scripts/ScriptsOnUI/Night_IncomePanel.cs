using NUnit.Framework;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Night_IncomePanel : MonoBehaviour
{
    public Player human;

    public GameObject IncomePanel;
    public GameObject CardCreatorPanel;
    public GameObject IncarnateCreatorPanel;
    public GameObject ConfirmButton;

    public TextMeshProUGUI PassiveDescription;

    public GameObject PurpleLP;
    public GameObject BlueLP;
    public GameObject GreenLP;
    public GameObject YellowLP;
    public GameObject OrangeLP;
    public GameObject RedLP;
    public GameObject PurpleRP;
    public GameObject BlueRP;
    public GameObject GreenRP;
    public GameObject YellowRP;
    public GameObject OrangeRP;
    public GameObject RedRP;

    public GameObject SkillCardMain;


    public static event Action<bool> CardCreatorMenu;
    public void OnEnable()
    {
        SunLevel.NightEvent += ShowIncome;


    }

    public void OnDisable()
    {
        SunLevel.NightEvent -= ShowIncome;
    }

    public void CreateSummary()
    {

    }

    public void ShowIncome()
    {
        IncomePanel.SetActive(true);
        CardCreatorPanel.SetActive(false);
        IncarnateCreatorPanel.SetActive(false);
    }

    public void ShowCardCreator()
    {
        IncomePanel.SetActive(false);
        CardCreatorPanel.SetActive(true);
        IncarnateCreatorPanel.SetActive(false);
        //FillTheFields(human.skills);
    }

    public void ShowIncarnateCreator()
    {
        IncomePanel.SetActive(false);
        CardCreatorPanel.SetActive(false);
        IncarnateCreatorPanel.SetActive(true);
       // FillTheCardFields(human.cards);
    }

    public void ShowConfirmButton()
    {
        ConfirmButton.SetActive(true);
    }

    public void ShowPlacesToBorn()
    {
        IncomePanel.SetActive(false);
        CardCreatorPanel.SetActive(false);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="skills"></param>
    //private void FillTheFields(IEnumerable<SkillScriptableObject> skills)
    //{
    //    foreach (var s in skills)
    //    {

    //        InstantiateSkillButton(s, s.RootedSkill);
    //    }
    //}
    /// <summary>
    /// 
    /// </summary>
    /// <param name="skill"></param>
    /// <param name="rootedSkill"></param>
    private void InstantiateSkillButton(SkillScriptableObject skill, bool rootedSkill)
    {
        if (rootedSkill)
        {

            switch (skill.actionType)
            {
                case ActionTypeEnum.Purple:
                    Instantiate(skill.ButtonPrefab, PurpleLP.transform);
                    break;
                case ActionTypeEnum.Blue:
                    Instantiate(skill.ButtonPrefab, BlueLP.transform);
                    break;
                case ActionTypeEnum.Green:
                    Instantiate(skill.ButtonPrefab, GreenLP.transform);
                    break;
                case ActionTypeEnum.Yellow:
                    Instantiate(skill.ButtonPrefab, YellowLP.transform);
                    break;
                case ActionTypeEnum.Orange:
                    Instantiate(skill.ButtonPrefab, OrangeLP.transform);
                    break;
                case ActionTypeEnum.Red:
                    Instantiate(skill.ButtonPrefab, RedLP.transform);
                    break;

            }

        }
        else if (!rootedSkill)
        {
            switch (skill.actionType)
            {
                case ActionTypeEnum.Purple:
                    Instantiate(skill.ButtonPrefab, PurpleRP.transform);
                    break;
                case ActionTypeEnum.Blue:
                    Instantiate(skill.ButtonPrefab, BlueRP.transform);
                    break;
                case ActionTypeEnum.Green:
                    Instantiate(skill.ButtonPrefab, GreenRP.transform);
                    break;
                case ActionTypeEnum.Yellow:
                    Instantiate(skill.ButtonPrefab, YellowRP.transform);
                    break;
                case ActionTypeEnum.Orange:
                    Instantiate(skill.ButtonPrefab, OrangeRP.transform);
                    break;
                case ActionTypeEnum.Red:
                    Instantiate(skill.ButtonPrefab, RedRP.transform);
                    break;

            }

        }


    }

    //private void FillTheCardFields(IEnumerable<CardScriptableObject> cards)
    //{
    //    foreach (var c in cards)
    //    {
    //        Instantiate(c);
    //    }
    //}

    

   
}
