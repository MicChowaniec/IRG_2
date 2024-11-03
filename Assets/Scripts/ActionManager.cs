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


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CreateButton(GameAction ga,GameObject parent)
    {
        GameObject go = Instantiate(ButtonPrefab, parent.transform);
        Button button = go.GetComponent<Button>();
        button.onClick.AddListener(ga.OnClick);
    }
    public void RefreshButtons(Turn turn)
    {
        //Add cleaning
        foreach (GameAction ga in tbs.activePlayer.GetComponent<PlayerScript>().actions)
        {
            //Add all cases
            CreateButton(ga, PurplePanel);
        }
    }
    public void CheckTurnAndPlayer()
    {
        if( tbs.ActiveTurn!=null)
        {
            RefreshButtons(tbs.ActiveTurn);
        }
        else
        {
            Debug.Log("haven't found name of the turn: " + tbs.ActiveTurn.nameOfTurn);
        }
    }
   


}
