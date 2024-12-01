using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonsManager : MonoBehaviour
{
   public void OnGameSettingsPressStart()
    {
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
}
