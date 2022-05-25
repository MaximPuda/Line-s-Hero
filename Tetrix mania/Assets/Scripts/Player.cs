using UnityEngine;

[System.Serializable]
public static class Player
{
    public static string playerName;
    public static string[] modes;
    public static int[] bestScores;
    public static int allLinesCount;

    public static void Save()
    {
        FileManager.SaveToFile();
    }

    public static bool Load()
    {
        var save = FileManager.LoadFromFile();
        if(save != null)
        {
            playerName = save.playerName;
            modes = save.modes;
            bestScores = save.bestScores;
            allLinesCount = save.allLinesCount;
            return true;
        }
        else return false;
    }

    public static void Reset()
    {
        var gameModes = GameObject.FindObjectOfType<GameModeChanger>().modes;
        modes = new string[gameModes.Length];
        bestScores = new int[gameModes.Length];

        for (int i = 0; i < modes.Length; i++)
            modes[i] = gameModes[i].modeName;

        playerName = "Player";
        allLinesCount = 0;

        Save();
    }

    public static int GetBestScore(string mode)
    {
        var index = GetIndexOfMode(mode);
        if (index != -1)
        {
            return bestScores[index];
        }
 
        return 0;
    }

    public static void SetBestScore(string mode, int score)
    {
        var index = GetIndexOfMode(mode);
        if (index != -1)
        {
            bestScores[index] = score;
            Save();
        }
        else
            Debug.Log("Game mode not found!");
    }

    private static int GetIndexOfMode(string mode)
    {
        int index = -1;
        for (int i = 0; i < modes.Length; i++)
        {
            if (modes[i].Contains(mode))
                index = i;
        }

        return index;
    }
}
