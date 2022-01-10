using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    public TextMeshProUGUI playerName;
    public TextMeshProUGUI lines;
    public TextMeshProUGUI bestScore;

    void Start()
    {
        playerName.text = PlayerStatic.PlayerName;
        lines.text = PlayerStatic.AllLineCleared.ToString();
        bestScore.text = PlayerStatic.BestScore.ToString();
    }

    public void DeleteSaveFile()
    {
        SaveSystem.DeleteSaveFile();
    }
}
