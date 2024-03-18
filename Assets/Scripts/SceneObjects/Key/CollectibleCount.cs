using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleCount : MonoBehaviour
{
    TMPro.TMP_Text text;
    int count;

    // Fading parameters
    public float fadeDuration = 1f;
    public float fadeDelay = 2f;

    bool isVisible = false; // Flag to track if the text is currently visible

    void Awake()
    {
        text = GetComponent<TMPro.TMP_Text>();
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0f); // Set initial alpha to zero
    }

    void Start()
    {
        UpdateCount();
    }

    void OnEnable() => NewBehaviourScript.OnCollected += OnCollevtibleCollected;
    void OnDisable() => NewBehaviourScript.OnCollected -= OnCollevtibleCollected;

    void OnCollevtibleCollected()
    {
        count++;
        UpdateCount();
        StartCoroutine(FadeText());
    }

    void UpdateCount()
    {
        text.text = $"{count} / {NewBehaviourScript.total}";
    }

    IEnumerator FadeText()
    {
        if (!isVisible)
        {
            // Fade in
            float elapsedTime = 0f;
            Color startColor = text.color;
            Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1f); // Fully opaque

            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                text.color = Color.Lerp(startColor, endColor, elapsedTime / fadeDuration);
                yield return null;
            }

            isVisible = true;
        }

        yield return new WaitForSeconds(fadeDelay);

        // Fade out
        float fadeOutTime = 0f;
        Color currentColor = text.color;
        Color transparentColor = new Color(currentColor.r, currentColor.g, currentColor.b, 0f); // Fully transparent

        while (fadeOutTime < fadeDuration)
        {
            fadeOutTime += Time.deltaTime;
            text.color = Color.Lerp(currentColor, transparentColor, fadeOutTime / fadeDuration);
            yield return null;
        }

        isVisible = false;
    }
}
