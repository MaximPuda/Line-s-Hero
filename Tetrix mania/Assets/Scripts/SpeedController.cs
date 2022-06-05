using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.Events;

public class SpeedController : MonoBehaviour
{
    [SerializeField] private BlockController blockController;
    [SerializeField] private ScoreSystem scoreSystem;
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private UnityEvent<float> OnSpeedChange;
    [SerializeField] private UnityEvent<string> ShowMessage;

    private bool isActive;
    private int speedLevel = 0;
    private float speedCoef;
    private int linesToSpeedUp;
    private float startSpeed = 1;
    private float newSpeed;
    private List<float> previousSpeeds;

    private float maxSpeed = 0.04f;

    private void Start()
    {
        isActive = GameModeSettings.speedLevelsActive;
        speedCoef = GameModeSettings.speedCoef;
        linesToSpeedUp = GameModeSettings.linesToSpeedUp;
        previousSpeeds = new List<float>();
    }

    public void CheckLevelUp(int lines)
    {
        if (isActive && lines % linesToSpeedUp == 0)
            SpeedUp();
    }

    public void SpeedUp()
    {
        if (isActive)
        {
            if (speedLevel == 0)
            {
                previousSpeeds.Add(startSpeed);
                newSpeed = Mathf.Lerp(startSpeed, maxSpeed, speedCoef);
            }
                
            else
            {
                previousSpeeds.Add(newSpeed);
                newSpeed = Mathf.Lerp(newSpeed, maxSpeed, speedCoef);
            }
                

            speedLevel++;

            OnSpeedChange.Invoke(newSpeed);
            ShowMessage.Invoke("SPEED UP");
            speedText.text = speedLevel.ToString();
        }
    }

    public void SpeedDown()
    {
        if (isActive)
        {
            if (previousSpeeds.Count > 0)
            {
                speedLevel--;
                newSpeed = previousSpeeds[previousSpeeds.Count - 1];
                previousSpeeds.RemoveAt(previousSpeeds.Count - 1);

                OnSpeedChange.Invoke(newSpeed);
                ShowMessage.Invoke("SPEED DOWN");
                speedText.text = speedLevel.ToString();
                Debug.Log($"New speed: {newSpeed}");
            }
            else Debug.Log("SpeedDown impossible!");
        }
    }
}
