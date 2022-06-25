using UnityEngine;

public class GameModeChanger : MonoBehaviour
{
    [SerializeField] public GameMode[] modes;

    public void SetGameMode(int index)
    {
        GameModeSettings.mode = modes[index].name;
        GameModeSettings.speedLevelsActive = modes[index].speedLevelsActive;
        GameModeSettings.linesToSpeedUp = modes[index].linesToSpeedUp;
        GameModeSettings.timerActive = modes[index].timerActive;
        GameModeSettings.speedCoef = modes[index].speedCoef;
        GameModeSettings.bgMusic = modes[index].bgMusic;
    }
}
