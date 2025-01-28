using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PopUpScript : MonoBehaviour
{
    public TextMeshProUGUI labelTMP;
    public TextMeshProUGUI descriptionTMP;
    public GameObject buttonOK;
    public Canvas parentCanvas;

    private Coroutine displayCoroutine;
    private Coroutine animationCoroutine;
    private bool isMouseOver = false; // Tracks if the mouse is over the object

    private void OnEnable()
    {
        OnHoverScript.OnHoverBroadcast += StartDisplayPopUp;
        OnHoverScript.HidePopUp += HidePopUP;
    }

    private void OnDisable()
    {
        OnHoverScript.OnHoverBroadcast -= StartDisplayPopUp;
        OnHoverScript.HidePopUp -= HidePopUP;
    }

    private void StartDisplayPopUp(string label, string description, bool button, GameObjectTypeEnum gote, ActionTypeEnum color)
    {
        isMouseOver = true;

        StopAllActiveCoroutines(); // Stop any existing coroutines
        displayCoroutine = StartCoroutine(DisplayPopUpWithDelay(label, description, button));
    }

    private IEnumerator DisplayPopUpWithDelay(string label, string description, bool button)
    {
        float delay = 0.5f;
        float elapsedTime = 0f;

        while (elapsedTime < delay)
        {
            if (!isMouseOver) yield break; // Exit if mouse is no longer over the object
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Calculate position relative to mouse
        Vector2 movePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform.parent.GetComponent<RectTransform>(),
            Input.mousePosition,
            parentCanvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : parentCanvas.worldCamera,
            out movePos
        );

        transform.localPosition = movePos + new Vector2(0, 100);

        // Update UI elements
        labelTMP.text = label;
        descriptionTMP.text = description;
        buttonOK.SetActive(button);

        // Animate pop-up
        animationCoroutine = StartCoroutine(AnimatePopUpScale(Vector3.zero, Vector3.one, 0.5f));
    }

    private IEnumerator AnimatePopUpScale(Vector3 startScale, Vector3 endScale, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            t = t * t * (3f - 2f * t); // SmoothStep easing
            transform.localScale = Vector3.Lerp(startScale, endScale, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = endScale;
    }

    public void OnClick()
    {
        Debug.Log("OK button clicked.");
        HidePopUP();
    }

    public void HidePopUP()
    {
        StopAllActiveCoroutines(); // Stop any ongoing coroutines
        transform.localScale = Vector3.zero;
        buttonOK.SetActive(false);
        isMouseOver = false; // Reset the flag
        Debug.Log("PopUp Closed");
    }

    private void StopAllActiveCoroutines()
    {
        if (displayCoroutine != null)
        {
            StopCoroutine(displayCoroutine);
            displayCoroutine = null;
        }
        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
            animationCoroutine = null;
        }
    }
}