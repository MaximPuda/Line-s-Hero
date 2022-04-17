using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private string playerName;
    [SerializeField] private int bestScore;
    [SerializeField] private int allLinesCleared;

    public string PlayerName
    {
        get { return playerName; }
        set { playerName = value; }
    }
    public int BestScore
    {
        get { return bestScore; }
        set 
        {
            if(bestScore < value)
                bestScore = value;
        }
    }
    public int AllLineCleared
    {
        get { return allLinesCleared; }
        set { allLinesCleared += value; }
    }

    public void SavePlayer()
    {
        //SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        if (data != null)
        {
            playerName = data.playerName;
            bestScore = data.bestScore;
            allLinesCleared = data.allLineCleared;
        }
        
    }

    public void DeletePlayer()
    {
        SaveSystem.DeleteSaveFile();
    }
}
