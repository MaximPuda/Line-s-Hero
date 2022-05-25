using UnityEngine;

[CreateAssetMenu(fileName = "New game mode")]
public class GameMode : ScriptableObject
{
    public string modeName;
    public bool speedLevelsActive = true;
    public bool timerActive = false;
    public float speedCoef = 0.15f;
    public int linesToSpeedUp;
}
