using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUpPanel : MonoBehaviour
{
    public GameObject panel; // Panel do wy�wietlenia
    public TextMeshProUGUI messageText; // Tekst w panelu

    // Zdarzenie do wywo�ania
    public delegate void OnPopUpPanel(string message);
    public static event OnPopUpPanel PopUp;

    private void OnEnable()
    {
        PopUp += ShowPanel;
    }

    private void OnDisable()
    {
        PopUp -= ShowPanel;
    }

    // Publiczna metoda do wywo�ywania zdarzenia
    public static void RaisePopUpPanel(string message)
    {
        PopUp?.Invoke(message);
    }

    // Metoda do wy�wietlania panelu
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
