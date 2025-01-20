using System;
using UnityEngine;

public class Day_Panel : MonoBehaviour
{
    public GameObject ActionsBar;
    public GameObject TopBar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnEnable()
    {
        SunLevel.DayEvent += DisplayCanvas;
        SunLevel.NightEvent += HideCanvas;

    }
    private void OnDisable()
    {
        SunLevel.DayEvent -= DisplayCanvas;
        SunLevel.NightEvent -= HideCanvas;
    }

    private void HideCanvas()
    {
        ActionsBar.SetActive(false);
        TopBar.SetActive(false);
    }

    private void DisplayCanvas(int obj)
    {
        ActionsBar.SetActive(true); 
        TopBar.SetActive(true);

    }
}
