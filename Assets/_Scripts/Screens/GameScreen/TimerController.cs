using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private Button startButton;
    
    public float TimerDuration { get; set; } = 60f;
    private float timer;
    private bool isTimerRunning;
    public event Action TimerEnded;

    private void Start()
    {
        startButton.onClick.AddListener(StartOrResetTimer);
        UpdateTimerText();
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            timer -= Time.deltaTime;
            UpdateTimerText();

            if (timer <= 0)
            {
                timer = 0;
                isTimerRunning = false;
                TimerEnded?.Invoke();
                UpdateTimerText();
            }
        }
    }

    private void StartOrResetTimer()
    {
        timer = TimerDuration;
        isTimerRunning = true;
        UpdateTimerText();
    }

    private void UpdateTimerText()
    {
        timerText.text = $"{(int)timer}s";
    }
}