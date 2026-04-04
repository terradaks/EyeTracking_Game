using UnityEngine;
using TMPro;
using System;
public class TimerScript : MonoBehaviour
{
    private bool _timerActive;
    private float _currentTime;
    [SerializeField] private TMP_Text _text;
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


    }
    public void StartTimer() //Public method to start Timer
    {
        _timerActive = true;
    }
    public void StopTimer()//Public method to stop Timer
    {
        _timerActive = false;
    } 
}
