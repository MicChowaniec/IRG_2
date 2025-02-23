using JetBrains.Annotations;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class ScoreKeeperScript : MonoBehaviour
{
    public Player humanPlayer;

    public int eyes;
    public TextMeshProUGUI eyesText;

    public Image waterTank;
    public TextMeshProUGUI waterText;
    public int water;

    public Image energyTank;
    public TextMeshProUGUI energyText;
    public int energy;

    public Image starlingTank;
    public TextMeshProUGUI starlingText;
    public int starling;
    public int maxStarling;

    public Image fireResistanceTank;
    public TextMeshProUGUI fireResistanceText;
    public int fireResistance;

    public Image toxicResistanceTank;
    public TextMeshProUGUI toxicResistanceText;
    public int toxicResistance;

    public Image proteinTank;
    public TextMeshProUGUI proteinText;
    public int protein;

    public int biomass;
    public TextMeshProUGUI biomassText;



    public int purpleGenom;
    public TextMeshProUGUI purpleGenomText;

    public int blueGenom;
    public TextMeshProUGUI blueGenomText;

    public int greenGenom;
    public TextMeshProUGUI greenGenomText;

    public int yellowGenom;
    public TextMeshProUGUI yellowGenomText;

    public int orangeGenom;
    public TextMeshProUGUI orangeGenomText;

    public int redGenom;
    public TextMeshProUGUI redGenomText;



    public int sunLevel;
    public TextMeshProUGUI sunLevelText;

    public int diseaseLevel;
    public TextMeshProUGUI diseaseText;

    public int redFields;
    public TextMeshProUGUI redFieldsText;
    public int orangeFields;
    public TextMeshProUGUI orangeFieldsText;
    public int yellowFields;
    public TextMeshProUGUI yellowFieldsText;
    public int greenFields;
    public TextMeshProUGUI greenFieldsText;
    public int blueFields;
    public TextMeshProUGUI blueFieldsText;
    public int purpleFields;
    public TextMeshProUGUI purpleFieldsText;


    public void OnEnable()
    {
        SunLevel.DayEvent += SunLevelChange;
        SunLevel.NightEvent += SunLevelChange;

        AbstractSkill.Change += Change;


        MapManager.CountedRootables += FieldsChange;
       

        PlayerManager.PlayersInstantiated += StartingParameters;


    }
    public void OnDisable()
    {
        SunLevel.DayEvent -= SunLevelChange;
        SunLevel.NightEvent -= SunLevelChange;

        AbstractSkill.Change -= Change;

        MapManager.CountedRootables -= FieldsChange;

        PlayerManager.PlayersInstantiated -= StartingParameters;
    }


    private void StartingParameters()
    {
        PlayerManager pm = FindAnyObjectByType<PlayerManager>();
        foreach (Player p in pm.players)
        {
            if(p.human==true)
            {
                humanPlayer = p;
                Change();
            }
        }
    }
    



    private void SunLevelChange(int sunLevelChange)
    {
        sunLevel = sunLevelChange;
        sunLevelText.text = sunLevel.ToString();
    }
    private void SunLevelChange()
    {
        sunLevel = 0;
        sunLevelText.text = sunLevel.ToString();
    }

 

    private void FieldsChange(int[] fieldsChange, int rootableFields)
    {
        int redFieldsChange = fieldsChange[5];
        redFields = redFieldsChange;
        redFieldsText.text = redFieldsChange + "/" + rootableFields + " (" + ChangeResource(redFieldsChange, rootableFields) + "%)";

        int orangeFieldsChange = fieldsChange[4];
        orangeFields = orangeFieldsChange;
        redFieldsText.text =  orangeFieldsChange + "/" + rootableFields +" ("+ ChangeResource(orangeFieldsChange, rootableFields) + "%)";
        
        int yellowFieldsChange = fieldsChange[3];
        yellowFields = yellowFieldsChange;
        yellowFieldsText.text =  + yellowFieldsChange + "/" + rootableFields + " (" + ChangeResource(yellowFieldsChange, rootableFields) + "%)";

        int greenFieldsChange =fieldsChange[2];
        yellowFields = greenFieldsChange;
        yellowFieldsText.text = + greenFieldsChange + "/" + rootableFields + " (" + ChangeResource(greenFieldsChange, rootableFields) + "%)";

        int blueFieldsChange = fieldsChange[1];
        blueFields = blueFieldsChange;
        blueFieldsText.text = +blueFieldsChange + "/" + rootableFields + " (" + ChangeResource(blueFieldsChange, rootableFields) + "%)";

        int purpleFieldsChange = fieldsChange[0];
        purpleFields = purpleFieldsChange;
        purpleFieldsText.text =  + purpleFieldsChange + "/" + rootableFields + "(" + ChangeResource(purpleFieldsChange, rootableFields) + "%)";
    }
    private float ChangeResource(int resourceChange)
    {
        return (float)resourceChange / (float)biomass;
    }

    private float ChangeResource(int resourceChange, int resourceMax)
    {
        return (float)resourceChange / (float)resourceMax;
    }

    private void Change()
    {
        purpleGenom = humanPlayer.PurpleLvl;
        purpleGenomText.text = "P:" + purpleGenom;
        blueGenom = humanPlayer.BlueLvl;
        blueGenomText.text = "B:" + blueGenom;
        greenGenom = humanPlayer.GreenLvl;
        greenGenomText.text = "G:" + greenGenom;
        yellowGenom = humanPlayer.YellowLvl;
        yellowGenomText.text = "Y:" + yellowGenom;
        orangeGenom = humanPlayer.OrangeLvl;
        orangeGenomText.text = "O:" + orangeGenom;
        redGenom = humanPlayer.RedLvl;
        redGenomText.text = "R:" + redGenom;

        eyes = humanPlayer.eyes;
        eyesText.text = "Eyes: \n" + eyes;

        water = humanPlayer.water;
        waterText.text = "Water: \n" + water + "/" + biomass;
        waterTank.fillAmount = ChangeResource(water);

        energy = humanPlayer.energy;
        energyText.text = "Energy: \n" + energy + "/" + biomass;
        energyTank.fillAmount = ChangeResource(energy);

        protein = humanPlayer.protein;
        proteinText.text = "Protein: \n" + protein + "/" + biomass;
        proteinTank.fillAmount = ChangeResource(protein);

        maxStarling = humanPlayer.soulLvl;
        starling = humanPlayer.starlings;
        starlingText.text = "Starlings: \n" + starling + "/" + maxStarling;
        starlingTank.fillAmount = ChangeResource(starling, maxStarling);

        biomass = humanPlayer.biomass;
        biomassText.text = "Biomass: \n" + biomass;



    }


}
