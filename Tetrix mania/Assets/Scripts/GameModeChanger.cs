using UnityEngine;

public class GameModeChanger : MonoBehaviour
{
    public void SetClassicMode()
    {
        GameModeSettings.Mode = "Classic";
        GameModeSettings.SpeedLevelsActive = true;
        GameModeSettings.speedCoef = 0.15f;
    }

    public void SetQuickMode()
    {
        GameModeSettings.Mode = "Quick";
        GameModeSettings.SpeedLevelsActive = true;
        GameModeSettings.TimerActive = true;
        GameModeSettings.speedCoef = 0.3f;
    }

    public void SetChillMode()
    {
        GameModeSettings.Mode = "Chill";
        GameModeSettings.SpeedLevelsActive = false;
        GameModeSettings.TimerActive = false;
    }
}
