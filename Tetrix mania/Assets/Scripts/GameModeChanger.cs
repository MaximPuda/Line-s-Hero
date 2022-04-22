using UnityEngine;

public class GameModeChanger : MonoBehaviour
{
    public void SetClassicMode()
    {
        GameModeSettings.SpeedLevelsActive = true;
    }

    public void SetQuickMode()
    {
        GameModeSettings.SpeedLevelsActive = true;
        GameModeSettings.TimerActive = true;
    }

    public void SetChillMode()
    {
        GameModeSettings.SpeedLevelsActive = false;
        GameModeSettings.TimerActive = false;
    }
}
