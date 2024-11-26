using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectionScript : MonoBehaviour
{
    public TextMeshProUGUI TitleText;
    public TextMeshProUGUI DescriptionText;
    public Player[] players;
    public Button next;
    public Button previous;
    public Button select;
    private Player displayedPlayer;
    public GameObject cameraRoot;
    private int displayedPlayerIndex;
    // Start is called before the first frame update
    void Start()
    {
        displayedPlayerIndex = 0;
        displayedPlayer = players[displayedPlayerIndex];
        foreach (Player p in players)
        {
            p.human= false;
        }
    }

    public void OnClickPick()
    {
        displayedPlayer.human = true;
        SceneManager.LoadScene("VsAIScene");
    }
    public void OnClickNext()
    {

        CheckForPlayer(1);
        StartCoroutine(UpdateDisplayedPlayerCoroutine(-60));
    }
    public void OnClickPrevious()
    {
        CheckForPlayer(-1);
        StartCoroutine(UpdateDisplayedPlayerCoroutine(60));
    }
    private IEnumerator UpdateDisplayedPlayerCoroutine(float angle)
    {
        next.gameObject.SetActive(false);
        previous.gameObject.SetActive(false);
        select.gameObject.SetActive(false);
        {
            Quaternion startRotation = cameraRoot.transform.rotation;
            Quaternion endRotation = Quaternion.Euler(cameraRoot.transform.eulerAngles + new Vector3(0, angle, 0));
            float duration = 1f; // Duration of the rotation
            float elapsed = 0f;

            while (elapsed < duration)
            {
                cameraRoot.transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null;
            }

            cameraRoot.transform.rotation = endRotation; // Ensure it reaches the final rotation
        }
        next.gameObject.SetActive(true);
        previous.gameObject.SetActive(true);
        select.gameObject.SetActive(true);
    }
    private void CheckForPlayer(int index)
    {
        displayedPlayerIndex += index;
        if (displayedPlayerIndex > players.Length-1)
        {
            displayedPlayerIndex = 0;

        }
        else if (displayedPlayerIndex < 0)
        {
            displayedPlayerIndex = players.Length - 1;
        }
        displayedPlayer = players[displayedPlayerIndex];

    }

}
