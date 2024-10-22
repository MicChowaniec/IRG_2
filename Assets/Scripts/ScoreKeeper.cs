using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ScoreKeeper : MonoBehaviour
{
    public TextMeshProUGUI bioMassUpdateText;
    public TextMeshProUGUI energyUpdateText;
    public TextMeshProUGUI waterUpdateText;
    public TextMeshProUGUI starlingsCounterText;

    public TextMeshProUGUI purpleText;
    public TextMeshProUGUI blueText;
    public TextMeshProUGUI greenText;
    public TextMeshProUGUI yellowText;
    public TextMeshProUGUI orangeText;
    public TextMeshProUGUI redText;
    
    public MapManager mapManager;

    public TurnBasedSystem tbs;

    
    private int redScore;
    private float redScoreP;
    private int blueScore;
    private float blueScoreP;
    private int greenScore;
    private float greenScoreP;
    private int yellowScore;
    private float yellowScoreP;
    private int orangeScore;
    private float orangeScoreP;
    private int purpleScore;
    private float purpleScoreP;

    public TextMeshProUGUI winnerLabel;
    public GameObject WinnerPanel;

    // Start is called before the first frame update
    void Start()
    {
        UpdateScore();
    }


    public void UpdateScore()
    {

        int tempIntRed = 0;
        int tempIntBlue = 0;
        int tempIntGreen = 0;
        int tempIntYellow = 0;
        int tempIntOrange = 0;
        int tempIntPurple = 0;

        foreach (TileScript g in mapManager.tiles.Values)
        {
            if (g != null)
            {
                if (g.owner == 1)
                {
                    tempIntPurple++;
                }
                else if (g.owner == 2)
                {
                    tempIntBlue++;
                }
                else if (g.owner == 3)
                {
                    tempIntGreen++;
                }
                else if (g.owner == 4)
                {
                    tempIntYellow++;
                }
                else if (g.owner == 5)
                {
                    tempIntOrange++;
                }
                else if (g.owner == 6)
                {
                    tempIntRed++;
                }

            }
        }
        redScore = tempIntRed;
        redScoreP = (float)tempIntRed / (float)mapManager.rootables * 100;
        blueScore = tempIntBlue;
        blueScoreP = (float)tempIntBlue / (float)mapManager.rootables * 100;
        greenScore = tempIntGreen;
        greenScoreP = (float)tempIntGreen / (float)mapManager.rootables * 100;
        yellowScore = tempIntYellow;
        yellowScoreP = (float)tempIntYellow / (float)mapManager.rootables * 100;
        orangeScore = tempIntOrange;
        orangeScoreP = (float)tempIntOrange / (float)mapManager.rootables * 100;
        purpleScore = tempIntPurple;
        purpleScoreP = (float)tempIntPurple / (float)mapManager.rootables * 100;

        if (redScoreP >= 51)
        {
            DisplayFinish("Red");
        }
        else
        {
            redText.text = DisplayText(redScore, redScoreP);
        }
        if (blueScoreP >= 51)
        {
            DisplayFinish("Blue");
        }
        else
        {
            blueText.text = DisplayText(blueScore, blueScoreP);
        }
        if (greenScoreP >= 51)
        {
            DisplayFinish("Green");
        }
        {
            greenText.text = DisplayText(greenScore, greenScoreP);
        }
        if (yellowScoreP >= 51)
        {
            DisplayFinish("Yellow");
        }
        else
        {
            yellowText.text = DisplayText(yellowScore, yellowScoreP);
        }
        if (orangeScoreP >= 51)
        {
            DisplayFinish("Orange");
        }
        else
        {
            orangeText.text = DisplayText(orangeScore, orangeScoreP);
        }
        if (purpleScoreP >= 51)
        {
            DisplayFinish("Purple");
        }
        else
        {
            purpleText.text  = DisplayText(purpleScore, purpleScoreP);
        }

    }
    public string DisplayText(int i, float f)
    {
        
        
        return i+"("+f+"%)";
    }

    public void DisplayFinish(string winner)
    {
        winnerLabel.text = winner + " player victory!";
        tbs.dayUI.SetActive(false);
        tbs.nightUI.SetActive(false);
        WinnerPanel.SetActive(true);
    }
    public void FinishGameClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
