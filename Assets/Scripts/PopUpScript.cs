using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PopUpScript : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    public TextMeshProUGUI labelTMP;
    public TextMeshProUGUI descriptionTMP;
    public GameObject buttonOK;
    public GameObject cameraGameObject;
    public Canvas parentCanvas;

    private Coroutine displayCoroutine;
    private Coroutine animationCoroutine;
    private bool isMouseOver = false; // Flag to track if the mouse is over the object

    private void OnEnable()
    {
        OnHoverScript.OnHoverBroadcast += StartDisplayPopUp;
    }
    private void OnDisable()
    {
        OnHoverScript.OnHoverBroadcast -= StartDisplayPopUp;
    }

    private void StartDisplayPopUp(string label, string description, bool button)
    {
        isMouseOver = true; // Set the flag to true when mouse is over
        if (displayCoroutine != null)
        {
            StopCoroutine(displayCoroutine);
        }
        displayCoroutine = StartCoroutine(DisplayPopUpWithDelay(label, description, button));
    }

    private IEnumerator DisplayPopUpWithDelay(string label, string description, bool button)
    {
        // Wait for 0.5 second
        float delay = 0.5f;
        float elapsedTime = 0f;

        while (elapsedTime < delay)
        {
            if (!isMouseOver)
            {
                // Stop coroutine if the mouse is no longer over the object
                yield break;
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Vector2 movePos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(),
             Input.mousePosition, parentCanvas.worldCamera,
            out movePos);

        transform.localPosition = movePos + new Vector2(0, 200);

        labelTMP.text = label;
        descriptionTMP.text = description;
        buttonOK.SetActive(button);

        // Start the scaling animation
        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
        }
        animationCoroutine = StartCoroutine(AnimatePopUpScale(Vector3.zero, Vector3.one, 0.5f));
    }

    private IEnumerator AnimatePopUpScale(Vector3 startScale, Vector3 endScale, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(startScale, endScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = endScale;
    }

    public void OnClick()
    {
        HidePopUP();
    }

    public void HidePopUP()
    {
        // Start the scaling animation in reverse
        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
        }
        animationCoroutine = StartCoroutine(AnimatePopUpScale(transform.localScale, Vector3.zero, 0.2f));

        buttonOK.SetActive(false);
        Debug.Log("PopUp Closed");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseOver = false; // Set the flag to false when mouse exits
        if (displayCoroutine != null)
        {
            StopCoroutine(displayCoroutine);
        }
        HidePopUP();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isMouseOver = true; // Set the flag to true when mouse re-enters
    }
}
