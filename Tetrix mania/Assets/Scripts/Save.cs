[System.Serializable]
public class Save
{
    public string playerName { get; private set; }
    public string[] modes { get; private set; }
    public int[] bestScores { get; private set; }
    public int allLinesCount{ get; private set; }

public Save ()
    {
        playerName = Player.playerName;
        modes = Player.modes;
        bestScores = Player.bestScores;
        allLinesCount = Player.allLinesCount;
    }
}
