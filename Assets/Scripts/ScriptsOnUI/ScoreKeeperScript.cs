using JetBrains.Annotations;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeperScript : MonoBehaviour
{
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

    public int rootableFields;


    public void OnEnable()
    {
        SunLevel.DayEvent += SunLevelChange;
        SunLevel.NightEvent += SunLevelChange;
        StarlingSkillScript.StarlingConsumed += StarlingChange;
        StarlingSkillScript.FishEaten += ChangeProteinLevel ;
    }
    public void OnDisable()
    {
        SunLevel.DayEvent -= SunLevelChange;
        SunLevel.NightEvent -= SunLevelChange;
        StarlingSkillScript.StarlingConsumed -= StarlingChange;
        StarlingSkillScript.FishEaten -= ChangeProteinLevel;
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

    private void ChangeWaterLevel(int waterChange)

    {
        water = waterChange;
        waterText.text = "Water" + waterChange + "/" + biomass;
        waterTank.fillAmount = ChangeResource(waterChange);
    }

    private void ChangeEnergyLevel(int energyChange)
    {
        energy = energyChange;
        energyText.text = "Energy" + energyChange + "/" + biomass;
        energyTank.fillAmount = ChangeResource(energyChange);
    }

    private void ChangeProteinLevel(int proteinChange)
    {
        protein = proteinChange;
        proteinText.text = "Protein" + proteinChange + "/" + biomass;
        proteinTank.fillAmount = ChangeResource(proteinChange);
    }

    private void StarlingChange(int starlingChange)
    {
        starling = starlingChange;
        starlingText.text = "Starlings" + starlingChange + "/" + maxStarling;
        starlingTank.fillAmount = ChangeResource(starlingChange, maxStarling);
    }
    private void BiomassChange(int biomassChange)
    {
        biomass = biomassChange;
        biomassText.text = "Biomass: \n" + biomassChange;
    }

    private void RedFieldsChange(int redFieldsChange)
    {
        redFields = redFieldsChange;
        redFieldsText.text = "Red: \n" + redFieldsChange + "/" + rootableFields + " (" + ChangeResource(redFieldsChange, rootableFields) + "%)";
    }

     private void OrangeFieldsChange(int  orangeFieldsChange)
    {
        orangeFields = orangeFieldsChange;
        redFieldsText.text = "Orange: \n" + orangeFieldsChange + "/" + rootableFields +" ("+ ChangeResource(orangeFieldsChange, rootableFields) + "%)";
    }

    private void YellowFieldsChange(int yellowFieldsChange)
    {
        yellowFields = yellowFieldsChange;
        yellowFieldsText.text = "Yellow: \n" + yellowFieldsChange + "/" + rootableFields + " (" + ChangeResource(yellowFieldsChange, rootableFields) + "%)";
    }
    private void GreenFieldsChange(int greenFieldsChange)
    {
        yellowFields = greenFieldsChange;
        yellowFieldsText.text = "Green: \n" + greenFieldsChange + "/" + rootableFields + " (" + ChangeResource(greenFieldsChange, rootableFields) + "%)";
    }
    private void BlueFieldsChange(int blueFieldsChange)
    {
        blueFields = blueFieldsChange;
        blueFieldsText.text = "Blue";
    }
    private float ChangeResource(int resourceChange)
    {
        return (float)resourceChange / (float)biomass;
    }

    private float ChangeResource(int resourceChange, int resourceMax)
    {
        return (float)resourceChange / (float)resourceMax;
    }


}
