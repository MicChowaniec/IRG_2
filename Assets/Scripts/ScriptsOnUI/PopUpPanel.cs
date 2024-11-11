using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PopUpPanel : MonoBehaviour
{
    public GameObject panel; // Panel do wyœwietlenia
    public TextMeshProUGUI messageText; // Tekst w panelu




    private void OnEnable()
    {
        //PopUp += ShowPanel;
    }

    private void OnDisable()
    {
       // PopUp -= ShowPanel;
    }

    // Publiczna metoda do wywo³ywania zdarzenia
    public static void RaisePopUpPanel(string message)
    {
       // PopUp?.Invoke(message);
    }

    // Metoda do wyœwietlania panelu
    private void ShowPanel(string message)
    {
        panel.SetActive(true);
        messageText.text = message;
    }
    public void OnButtonClick()
    {
        OnDisable();
    }
}
