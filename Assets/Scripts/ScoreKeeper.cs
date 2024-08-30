using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField]
    public GameObject textObject; // Reference to the UI Text object

    private TextMeshProUGUI textMeshPro; // Cache the TextMeshProUGUI component

    public MapManager mapManager;

    public int redScore;

    // Start is called before the first frame update
    void Start()
    {
        // Get the TextMeshProUGUI component from the textObject
        textMeshPro = textObject.GetComponent<TextMeshProUGUI>();

        if (textMeshPro == null)
        {
            Debug.LogError("TextMeshProUGUI component not found on the textObject!");
            return;
        }
        else
        {
            textMeshPro.text = "0(0%)";
        }

    }

    // Update is called once per frame
    public void UpdateScore()
    {
        int temp=0;
        foreach (TileScript g in mapManager.tiles.Values)
        {
            if (g != null)
            {
                if (g.owner == 1)
                {
                    temp++;
                }

            }
        }
        redScore = temp;
        textMeshPro.text = redScore.ToString();
    }
}
