using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ScoreKeeper : MonoBehaviour
{

    public GameObject redTextObject; // Reference to the UI Text object

    public GameObject blueTextObject; // Reference to the UI Text object

    private TextMeshProUGUI blueTextMeshPro; // Cache the TextMeshProUGUI component
    private TextMeshProUGUI redTextMeshPro; // Cache the TextMeshProUGUI component


    public MapManager mapManager;

    public TurnBasedSystem tbs;


    public int redScore;
    public float redScoreP;
    public int blueScore;
    public float blueScoreP;


    public GameObject WinnerPanel;

    // Start is called before the first frame update
    void Start()
    {
        // Get the TextMeshProUGUI component from the textObject
        redTextMeshPro = redTextObject.GetComponent<TextMeshProUGUI>();
        blueTextMeshPro = blueTextObject.GetComponent<TextMeshProUGUI>();

        if (redTextMeshPro == null)
        {
            Debug.LogError("TextMeshProUGUI component not found on the textObject!");
            return;
        }
        else
        {
            redTextMeshPro.text = "0(0%)";
        }
        if (redTextMeshPro == null)
        {
            Debug.LogError("TextMeshProUGUI component not found on the textObject!");
            return;
        }
        else
        {
            blueTextMeshPro.text = "0(0%)";
        }

    }


    public void UpdateScore()
    {
        int tempIntRed = 0;
        int tempIntBlue = 0;

        foreach (TileScript g in mapManager.tiles.Values)
        {
            if (g != null)
            {
                if (g.owner == 1)
                {
                    tempIntRed++;
                }
                else if (g.owner == 4)
                {
                    tempIntBlue++;
                }

            }
        }
        redScore = tempIntRed;
        redScoreP = (float)tempIntRed / (float)mapManager.rootables * 100;
        Debug.Log(redScoreP);
        redTextMeshPro.text = redScore.ToString() + "(" + redScoreP.ToString("F0") + "%)";
        if (redScoreP >= 51)
        {
            WinnerPanel.SetActive(true);
            tbs.dayUI.SetActive(false);
            tbs.nightUI.SetActive(false);

        }

        blueScore = tempIntBlue;
        blueScoreP = (float)tempIntBlue / (float)mapManager.rootables * 100;
        Debug.Log(blueScoreP);
        blueTextMeshPro.text = blueScore.ToString() + "(" + blueScoreP.ToString("F0") + "%)";
    }
    public void FinishGameClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
