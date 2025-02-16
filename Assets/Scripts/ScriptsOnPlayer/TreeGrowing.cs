using System.Collections;
using UnityEditor;
using UnityEngine;
using System;

public class TreeGrowing : MonoBehaviour
{
    public Vector3 scale;
    [SerializeField]
    private float size;
    public Entity owner;
    public Player activePlayer;

    private Coroutine sizeChangeCoroutine;

    public static event Action GrowingEnded;

    private void OnEnable()
    {
        
        Entity.GrowTheTree += OnSizeChange;
        scale = new Vector3(0.05f, 0.05f, 0.05f);
        size = 1;
        this.transform.localScale = scale;
    }

    private void OnDisable()
    {
        Entity.GrowTheTree -= OnSizeChange;
    }

    public void OnSizeChange(float newSize)
    {
        // Stop any existing size change animation before starting a new one
        if (sizeChangeCoroutine != null)
        {
            StopCoroutine(sizeChangeCoroutine);
        }

        sizeChangeCoroutine = StartCoroutine(AnimateSizeChange(newSize, 1f));
    }
    public void OnSizeChange(int newSizeUnNormalized)
    {

        if (owner == activePlayer)
        {
            float newSize = size + (float)newSizeUnNormalized / 10.0f;
            // Stop any existing size change animation before starting a new one
            OnSizeChange(newSize);
        }
    }

    private IEnumerator AnimateSizeChange(float targetSize, float duration)
    {
        float currentSize = size;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            size = Mathf.Lerp(currentSize, targetSize, elapsedTime / duration);
            this.transform.localScale = scale * size;
            yield return null;
        }

        // Ensure the final size is set
        size = targetSize;
        this.transform.localScale = scale * size;

        sizeChangeCoroutine = null;
        GrowingEnded?.Invoke();

    }
}
