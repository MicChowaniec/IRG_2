using System.Collections;
using TMPro;
using UnityEngine;

public class PopUpScript : MonoBehaviour
{
    public TextMeshProUGUI labelTMP;
    public TextMeshProUGUI descriptionTMP;
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

    private void StartDisplayPopUp(OnHoverSC onHoverSC)
    {
        isMouseOver = true;

        StopAllActiveCoroutines(); // Stop any existing coroutine
        displayCoroutine = StartCoroutine(DisplayPopUpWithDelay(onHoverSC.Label(), onHoverSC.Descripton()));
    }

    private IEnumerator DisplayPopUpWithDelay(string label, string description)
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
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform.parent.GetComponent<RectTransform>(),
            Input.mousePosition,
            parentCanvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : parentCanvas.worldCamera,
            out Vector2 movePos
        );

        transform.localPosition = movePos + new Vector2(0, 100);

        // Update UI elements
        labelTMP.text = label;
        descriptionTMP.text = description;
      
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
        isMouseOver = false; // Reset the flag
        Debug.Log("PopUp Closed");
    }
    /// <summary>
    /// Stopping all active coroutines in PopUpScript
    /// </summary>
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