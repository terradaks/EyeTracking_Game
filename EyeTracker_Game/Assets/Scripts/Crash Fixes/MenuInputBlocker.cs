using UnityEngine;
using System.Collections;

public class MenuInputBlocker : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float delay = 0.5f; // adjust as needed

    void Start()
    {
        // Block input
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        // Start delay
        StartCoroutine(EnableInputAfterDelay());
    }

    IEnumerator EnableInputAfterDelay()
    {
        yield return new WaitForSeconds(delay);

        // Re-enable input
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }
}