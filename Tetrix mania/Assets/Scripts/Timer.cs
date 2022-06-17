using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;


public class Timer : MonoBehaviour
{
    [SerializeField] private int minutes;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Slider slider;
    [SerializeField] private UnityEvent onStopTimer;

    private bool isRunning;
    private float time;

    private void Start()
    {
        if (GameModeSettings.timerActive)
        {
            timerText.gameObject.SetActive(true);
            slider.gameObject.SetActive(true);

            time = minutes * 60;
            slider.maxValue = time;
            StartTimer();
        }
    }

    private void Update()
    {
        if (isRunning && !GameManager.isGameEnded)
        {
            if (time > 1)
            {
                time -= Time.deltaTime;
                UpdateTimerText();
            }
            else StopTimer();
        }
    }

    private void UpdateTimerText()
    {
        var minutes = Mathf.FloorToInt(time / 60);
        var seconds = Mathf.FloorToInt(time % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        slider.value = time;
    }

    public void StartTimer()
    {
        isRunning = true;   
    }

    public void PauseTimer()
    {
        isRunning = false;
    }

    public void StopTimer()
    {
        isRunning = false;
        time = minutes * 60;
        onStopTimer.Invoke();
    }
}
