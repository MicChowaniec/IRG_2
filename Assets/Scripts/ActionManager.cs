using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ActionManager : MonoBehaviour
{
    public GameObject PurplePanel;

    public GameObject BluePanel;

    public GameObject GreenPanel;

    public GameObject YellowPanel;

    public GameObject OrangePanel;

    public GameObject RedPanel;

    [SerializeField]
    TurnBasedSystem tbs;

    public GameObject ButtonPrefab;


    public void CreateButton(GameAction ga, GameObject parent)
    {
        GameObject go = Instantiate(ButtonPrefab, parent.transform);
        Button button = go.GetComponent<Button>();
        button.onClick.AddListener(ga.OnClick);
        go.GetComponent<Image>().sprite = ga.Sprite;
    }
    public void RefreshButtons()
    {
        // Dictionary to map action types to their corresponding panels
        var panelMapping = new Dictionary<ActionTypeEnum, GameObject>
    {
        { ActionTypeEnum.Purple, PurplePanel },
        { ActionTypeEnum.Blue, BluePanel },
        { ActionTypeEnum.Green, GreenPanel },
        { ActionTypeEnum.Yellow, YellowPanel },
        { ActionTypeEnum.Orange, OrangePanel },
        { ActionTypeEnum.Red, RedPanel }
    };

        // Clear existing buttons from each panel
        foreach (var panel in panelMapping.Values)
        {
            foreach (Transform child in panel.transform)
            {
                Destroy(child.gameObject);
            }
        }

        // Create buttons for each action based on its action type
        foreach (GameAction ga in tbs.activePlayer.GetComponent<PlayerScript>().actions)
        {
            if (panelMapping.TryGetValue(ga.Type1, out GameObject panel))
            {
                CreateButton(ga, panel);  // Create button on the corresponding panel
            }
            else
            {
                Debug.LogWarning($"Unhandled action type: {ga.Type1}");  // Log if action type has no mapped panel
            }
        }
    }
    public void CheckTurnAndPlayer()
    {
        if (tbs.ActiveTurn != null && tbs.activePlayer.GetComponent<PlayerScript>().picked == true)
        {
            RefreshButtons();
        }
        else
        {
            Debug.Log("haven't found name of the turn: " + tbs.ActiveTurn.nameOfTurn);
        }
    }
   


}
