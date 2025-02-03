using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.GPUSort;

public class Day_Panel : MonoBehaviour
{
    public GameObject ActionsBar;
    public GameObject TopBar;
    public GameObject LeftBar;
    public GameObject TabButton;
    public GameObject ActionDescription;
    public TextMeshProUGUI ActionDescriptionText;
    public Player human;
    private List<GameObject> buttonsInstantiated = new();
    public GameObject EndTurnButton;



    public bool leftBarVisible;
    private bool isAnimating = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnEnable()
    {
        OnHoverScript.ShowSkillDescription += ShowActionDescription;
        OnHoverScript.HideSkillDescription += HideActionDescription;
        leftBarVisible = false;
        SunLevel.DayEvent += DisplayCanvas;
        SunLevel.NightEvent += HideCanvas;
        PlayerManager.ActivePlayerBroadcast += UpdateHuman;

    }

  

    private void OnDisable()
    {
        OnHoverScript.ShowSkillDescription -= ShowActionDescription;
        OnHoverScript.HideSkillDescription -= HideActionDescription;
        SunLevel.DayEvent -= DisplayCanvas;
        SunLevel.NightEvent -= HideCanvas;
        PlayerManager.ActivePlayerBroadcast -= UpdateHuman;
    }

    private void ShowActionDescription(string arg1, string arg2)
    {

        ActionDescription.SetActive(true);
        ActionDescriptionText.text = arg2;
    }

    private void HideActionDescription()
    {
        ActionDescription.SetActive(false);
        ActionDescriptionText.text = "";
    }

    private void HideCanvas()
    {
        ActionsBar.SetActive(false);
        TopBar.SetActive(false);
        LeftBar.SetActive(false);
        TabButton.SetActive(false);
    }

    private void DisplayCanvas(int obj)
    {
        ActionsBar.SetActive(true);
        TopBar.SetActive(true);
        LeftBar.SetActive(true) ;
        TabButton.SetActive(true);

    }
    public void Update()
    {

        if (Input.GetKeyDown(KeyCode.Tab) && !isAnimating) // Trigger only on key press
        {
            ToggleLeftBar();
        }
    }
    public void ToggleLeftBar()
    {
        leftBarVisible = !leftBarVisible;
        int signature = leftBarVisible ? 1 : -1;

        RectTransform rectTransform = LeftBar.GetComponent<RectTransform>();
        Vector3 startPosition = rectTransform.anchoredPosition;
        Vector3 endPosition = new Vector3(signature * 200, rectTransform.anchoredPosition.y, 0);

        StartCoroutine(MoveTheUI(LeftBar, startPosition, endPosition, 0.5f));
    }
    private IEnumerator MoveTheUI(GameObject obj, Vector3 start, Vector3 end, float duration)
    {
        isAnimating = true;
        RectTransform rectTransform = obj.GetComponent<RectTransform>(); // Get RectTransform for UI

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            rectTransform.anchoredPosition = Vector3.Lerp(start, end, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rectTransform.anchoredPosition = end; // Ensure the final position is set
        isAnimating = false;
    }
    private void AttachSkills()
    {

    }
    private void UpdateHuman(Player player)
    {
        if (player.human)
        {

            human = player;
            foreach (var s in player.cards)
            {
                if (player.rooted)
                {
                    Debug.Log(s.ToString());
                    buttonsInstantiated.Add(Instantiate(s.skillRooted, ActionsBar.transform));
                }
                else
                {
                    buttonsInstantiated.Add(Instantiate(s.skillNotRooted, ActionsBar.transform));
                }
            }
            buttonsInstantiated.Add(EndTurnButton);
        }
        else
        {
            foreach (var obj in buttonsInstantiated)
            {
                Destroy(obj);
            }
            buttonsInstantiated.Clear();

        }

    }


}
