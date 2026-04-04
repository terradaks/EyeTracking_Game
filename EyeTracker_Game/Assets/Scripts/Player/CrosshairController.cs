using UnityEngine;
using System.Collections;

public class CrosshairController : MonoBehaviour
{
    public RectTransform crosshair;      
    public Transform[] rowPositions;     
    private int currentRow = 1;
    private Vector2 targetPosition;
    public Canvas canvas;

    // Track currently fading coroutine for each row
    private Coroutine[] fadeCoroutines;

    void Start()
    {
        fadeCoroutines = new Coroutine[rowPositions.Length];
        Cursor.visible = true; // set false to hide cursor
        MoveToRow();
        HighlightRow(currentRow);
    }

    void Update()
    {
        HandleRowInput();
        HandleMouseX();

        // Smooth vertical snapping
        crosshair.anchoredPosition = Vector2.Lerp(crosshair.anchoredPosition, targetPosition, Time.deltaTime * 10f);
    }

    void HandleRowInput()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.W))
        {
            currentRow++;
            currentRow = Mathf.Clamp(currentRow, 0, rowPositions.Length - 1);
            MoveToRow();
            HighlightRow(currentRow);
        }

        if (UnityEngine.Input.GetKeyDown(KeyCode.S))
        {
            currentRow--;
            currentRow = Mathf.Clamp(currentRow, 0, rowPositions.Length - 1);
            MoveToRow();
            HighlightRow(currentRow);
        }
    }

    void HandleMouseX()
    {
        // Convert screen X to Canvas local X
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out localPoint);
        targetPosition.x = localPoint.x;
    }

    void MoveToRow()
    {
        Vector3 worldPos = rowPositions[currentRow].position;
        Vector2 viewportPos = Camera.main.WorldToViewportPoint(worldPos);
        RectTransform canvasRect = canvas.transform as RectTransform;

        float canvasY = (viewportPos.y - 0.5f) * canvasRect.sizeDelta.y;
        targetPosition.y = canvasY;
    }

    // fade in/out quickly
    void HighlightRow(int rowIndex)
    {
        Debug.Log($"Highlighting row {rowIndex}");
        // Stop previous fade on this row only
        if (fadeCoroutines[rowIndex] != null)
            StopCoroutine(fadeCoroutines[rowIndex]);

        // Start new fade
        fadeCoroutines[rowIndex] = StartCoroutine(FadeRow(rowIndex));
    }

    IEnumerator FadeRow(int rowIndex)
    {
        SpriteRenderer row = rowPositions[rowIndex].GetComponent<SpriteRenderer>();

        float duration = 0.2f;
        float timer = 0f;
        Color c = row.color;
        float maxAlpha = 0.04f; // subtle fade

        // Fade in
        while (timer < duration)
        {
            timer += Time.deltaTime;
            row.color = new Color(c.r, c.g, c.b, Mathf.Lerp(0f, maxAlpha, timer / duration));
            yield return null;
        }

        // Hold briefly
        yield return new WaitForSeconds(0.01f);

        timer = 0f;

        // Fade out
        while (timer < duration)
        {
            timer += Time.deltaTime;
            row.color = new Color(c.r, c.g, c.b, Mathf.Lerp(maxAlpha, 0f, timer / duration));
            yield return null;
        }

        row.color = new Color(c.r, c.g, c.b, 0f); // ensure fully invisible
        fadeCoroutines[rowIndex] = null; // clear reference
    }
}