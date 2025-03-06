using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using Unity.Services.Core;

public class MainMenuButtonsManager : MonoBehaviour
{
    public GameSettings gameSettings;
    public static event Action<string> DisplayConfirmationPopup;
    public static event Action<string> DisplayDissapearingPopup;

   
    public void OnGameSettingsPressStart()
    {
        ParametersEvent parametersEvent = new()
        {
            MapSize = gameSettings.SizeOfMap,
            CyclesPerDay = gameSettings.CyclesPerDay,
            Difficulty = gameSettings.Difficulty,
        };
        AnalyticsService.Instance.RecordEvent(parametersEvent);


        SceneManager.LoadScene("SelectionScene");
    }
    public void VsAISceneButtonClick()
    {
        SceneManager.LoadScene("vsAIScene");
    }
    public void ExitButtonClick()
    {

        Application.Quit();
    }

    public void PolicyConfirmed()
    {
        UnityServices.InitializeAsync();
        AnalyticsService.Instance.StartDataCollection();
        DisplayDissapearingPopup?.Invoke("Data collection started...");
    }

    public void OnRequestStopDataCollection()
    {
        AnalyticsService.Instance.StopDataCollection();
        DisplayConfirmationPopup?.Invoke("Data collection stopped...");
    }

    public void OnRequestDataDeletion()
    {
        AnalyticsService.Instance.RequestDataDeletion();
        DisplayConfirmationPopup?.Invoke("Data deleted!");
    }
}
