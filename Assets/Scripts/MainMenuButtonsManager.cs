using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonsManager : MonoBehaviour
{
   public void TutorialButtonClick()
    {
        SceneManager.LoadScene("TutorialScene");
    }
    public void HotSeatButtonClick()
    {
        SceneManager.LoadScene("HotSeatScene");
    }
    public void ExitButtonClick()
    {
        Application.Quit();
    }
}
