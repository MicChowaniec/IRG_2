using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using Unity.Services.Core;

public class HonkeyBonkeySceneManager: MonoBehaviour
{
    public GameObject WinnerPanel;
    public int time;


    public void VsAISceneButtonClick()
    {
        SceneManager.LoadScene("vsAIScene",LoadSceneMode.Single);
    }
    public void ExitButtonClick()
    {

        Application.Quit();
    }
    public void Update()
    {
        if (WinnerPanel.activeSelf)
        {
            time++;
        }
        if(time>100)
        {
            VsAISceneButtonClick();
        }
    }




}
