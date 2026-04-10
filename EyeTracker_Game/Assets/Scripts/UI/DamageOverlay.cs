using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DamageOverlay : MonoBehaviour
{
    public Image overlayImage;
    public float flashDuration = 0.2f;
    public float maxAlpha = 0.4f;

    public void Flash()
    {
        StopAllCoroutines();
        StartCoroutine(FlashRoutine());
    }

    IEnumerator FlashRoutine()
    {
        Color c = overlayImage.color;

        // Fade in
        float t = 0;
        while (t < flashDuration)
        {
            t += Time.unscaledDeltaTime;
            overlayImage.color = new Color(c.r, c.g, c.b, Mathf.Lerp(0, maxAlpha, t / flashDuration));
            yield return null;
        }

        // Fade out
        t = 0;
        while (t < flashDuration)
        {
            t += Time.unscaledDeltaTime;
            overlayImage.color = new Color(c.r, c.g, c.b, Mathf.Lerp(maxAlpha, 0, t / flashDuration));
            yield return null;
        }

        overlayImage.color = new Color(c.r, c.g, c.b, 0);
    }
}