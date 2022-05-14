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
    [SerializeField] private int linesToSpeedUp = 10;

    private bool isActive;
    private int speedLevel = 0;
    private float speedCoef;
    private float startSpeed = 1;
    private float newSpeed;
    private List<float> previousSpeeds;

    private float maxSpeed = 0.04f;

    private void Start()
    {
        isActive = GameModeSettings.SpeedLevelsActive;
        speedCoef = GameModeSettings.speedCoef;
        previousSpeeds = new List<float>();

        Debug.Log($"Speed Coef:  {speedCoef}");
    }

    public void CheckLevelUp(int lines)
    {
        if (lines % linesToSpeedUp == 0)
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
            speedText.text = speedLevel.ToString();
            Debug.Log($"New speed: {newSpeed}");
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
                speedText.text = speedLevel.ToString();
                Debug.Log($"New speed: {newSpeed}");
            }
            else Debug.Log("SpeedDown impossible!");
        }
    }
}
