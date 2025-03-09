using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using Unity.Services.Core;

public class HonkeyBonkeySceneManager: MonoBehaviour
{
 


    public void VsAISceneButtonClick()
    {
        SceneManager.LoadScene("vsAIScene",LoadSceneMode.Single);
    }
    public void ExitButtonClick()
    {

        Application.Quit();
    }

   

   
}
