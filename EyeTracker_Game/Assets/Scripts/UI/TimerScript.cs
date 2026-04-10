using UnityEngine;
using TMPro;
using System;
using System.Collections;
public class TimerScript : MonoBehaviour
{
    private bool _timerActive;
    private float _currentTime;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeSpeed = 2f;
    [SerializeField] private float visibleAlpha = 1f;
    [SerializeField] private float hiddenAlpha = 0f;
    [SerializeField] private EnemySpawner spawner;
    private int lastMinute = -1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _currentTime = 0;
        _timerActive = true; //Starts Timer when Unity play is pressed (remove this if we have a main menu, use public methods below.
    }

    // Update is called once per frame
    void Update()
    {
        if (_timerActive)
        {
            _currentTime = _currentTime + Time.deltaTime;
        }

        TimeSpan time = TimeSpan.FromSeconds(_currentTime);

        _text.text = time.Minutes.ToString() + ":" + time.Seconds.ToString() + ":" + time.Milliseconds.ToString();

            // Fade logic
        if (time.Seconds >= 55)
            FadeTo(visibleAlpha);
        else
            FadeTo(hiddenAlpha);

        // Minute trigger (once per minute)
        if (time.Minutes != lastMinute && time.Seconds == 0)
        {
            lastMinute = time.Minutes;
            StartCoroutine(MinuteEvent());
        }
    }
    public void StartTimer() //Public method to start Timer
    {
        _timerActive = true;
    }
    public void StopTimer()//Public method to stop Timer
    {
        _timerActive = false;
    } 

    void FadeTo(float target)
    {
        canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, target, Time.deltaTime * fadeSpeed);
    }

    IEnumerator MinuteEvent()
    {
        Debug.Log("Minute reached!");

        // Pause gameplay
        Time.timeScale = 0f;

        // Stop spawning
        if (spawner != null)
            spawner.enabled = false;

        // Flash effect
        yield return StartCoroutine(FlashTimer());

        // Change difficulty
        IncreaseDifficulty();

        // Small pause
        yield return new WaitForSecondsRealtime(0.5f);

        // Resume
        Time.timeScale = 1f;

        if (spawner != null)
            spawner.enabled = true;
    }

    IEnumerator FlashTimer()
    {
        for (int i = 0; i < 3; i++)
        {
            canvasGroup.alpha = 0f;
            yield return new WaitForSecondsRealtime(0.1f);

            canvasGroup.alpha = 1f;
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }

    void IncreaseDifficulty()
    {
        Debug.Log("Difficulty increased!");

        if (spawner != null)
        {
            spawner.spawnDelay *= 0.9f; // faster spawning
        }
    }
}
