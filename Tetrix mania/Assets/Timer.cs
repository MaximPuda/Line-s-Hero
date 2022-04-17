using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{
    [SerializeField] private int minutes;
    
    public TextMeshProUGUI timerLabel;
    public Slider slider;

    private bool isRunning;
    private float time;

    private void Start()
    {
        time = minutes * 60;
        slider.maxValue = time;
        StartTimer();
    }

    private void Update()
    {
        if (isRunning)
        {
            if (time > 1)
            {
                time -= Time.deltaTime;
                UpdateTimerLabel();
            }
            else StopTimer();
        }
    }

    private void UpdateTimerLabel()
    {
        var minutes = Mathf.FloorToInt(time / 60);
        var seconds = Mathf.FloorToInt(time % 60);

        timerLabel.text = string.Format("{0:00}:{1:00}", minutes, seconds);

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
    }
}
