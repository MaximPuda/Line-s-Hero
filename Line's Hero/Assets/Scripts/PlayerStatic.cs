using UnityEngine;

[System.Serializable]
public static class PlayerStatic
{
    private static string playerName;
    private static int bestScore;
    private static int allLinesCleared;

    public static string PlayerName
    {
        get { return playerName; }
        set { playerName = value; }
    }
    public static int BestScore
    {
        get { return bestScore; }
        set 
        {
            if(bestScore < value)
                bestScore = value;
        }
    }
    public static int AllLineCleared
    {
        get { return allLinesCleared; }
        set { allLinesCleared += value; }
    }

    public static void SavePlayer()
    {
        SaveSystem.SavePlayer();
    }

    public static bool LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        if (data != null)
        {
            playerName = data.playerName;
            bestScore = data.bestScore;
            allLinesCleared = data.allLineCleared;
            return true;
        }
        else return false;
    }

    public static void DeletePlayer()
    {
        SaveSystem.DeleteSaveFile();
    }
}
