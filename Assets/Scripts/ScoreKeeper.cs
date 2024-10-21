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


    public int redScore;
    public float redScoreP;
    public int blueScore;
    public float blueScoreP;
    public int greenScore;
    public float greenScoreP;
    public int yellowScore;
    public float yellowScoreP;
    public int orangeScore;
    public float orangeScoreP;
    public int purpleScore;
    public float purpleScoreP;


    public GameObject WinnerPanel;

    // Start is called before the first frame update
    void Start()
    {
        
        // Get the TextMeshProUGUI component from the textObject

        if (redText == null)
        {
            Debug.LogError("TextMeshProUGUI component not found on the textObject!");
            return;
        }
        else
        {
            redText.text = "0(0%)";
        }
        if (redText == null)
        {
            Debug.LogError("TextMeshProUGUI component not found on the textObject!");
            return;
        }
        else
        {
            blueText.text = "0(0%)";
        }

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
                else if (g.owner ==3)
                {
                    tempIntGreen++;
                }
                else if (g.owner == 4)
                {
                    tempIntYellow++;
                }
                else if (g.owner ==5)
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


        redText.text = redScore.ToString() + "(" + redScoreP.ToString("F0") + "%)";
        if (redScoreP >= 51)
        {
            WinnerPanel.SetActive(true);
            tbs.dayUI.SetActive(false);
            tbs.nightUI.SetActive(false);

        }

        blueScore = tempIntBlue;
        blueScoreP = (float)tempIntBlue / (float)mapManager.rootables * 100;
        Debug.Log(blueScoreP);
        blueText.text = blueScore.ToString() + "(" + blueScoreP.ToString("F0") + "%)";
    }
    public void FinishGameClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
