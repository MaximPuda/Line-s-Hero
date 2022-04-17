using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string playerName;
    public int bestScore;
    public int allLineCleared;

    public PlayerData ()
    {
        playerName = PlayerStatic.PlayerName;
        bestScore = PlayerStatic.BestScore;
        allLineCleared = PlayerStatic.AllLineCleared;
    }
}
