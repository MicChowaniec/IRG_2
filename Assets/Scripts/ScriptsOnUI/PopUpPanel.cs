using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PopUpPanel : MonoBehaviour
{
    public GameObject panel; // Panel do wyświetlenia
    public TextMeshProUGUI messageText; // Tekst w panelu




    private void OnEnable()
    {
        //PopUp += ShowPanel;
    }

    private void OnDisable()
    {
       // PopUp -= ShowPanel;
    }

    // Publiczna metoda do wywoływania zdarzenia
    public static void RaisePopUpPanel(string message)
    {
       // PopUp?.Invoke(message);
    }

    // Metoda do wyświetlania panelu
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
