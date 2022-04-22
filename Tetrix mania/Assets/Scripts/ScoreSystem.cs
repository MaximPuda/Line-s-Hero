using System;
using UnityEngine;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private int scoreOneLine = 100;
    [SerializeField] private int scoreTwoLine = 300;
    [SerializeField] private int scoreThreeLine = 700;
    [SerializeField] private int scoreFourLine = 1500;

    public TextMeshProUGUI linesText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI resultLinesText;
    public TextMeshProUGUI resultScoreText;
    public UIController uiController;

    private static int lines;
    private static int score;

    public int Lines { get { return lines; } }

    static void Start()
    {
        ResetScore();
    }

    public static void ResetScore()
    {
        lines = 0;
        score = 0;
    }

    public void AddLines()
    {
        lines++;
        OutScoreToHud();
    }

    private void OutScoreToHud()
    {
       linesText.text = Convert.ToString(lines);
       scoreText.text = Convert.ToString(score);
    }

    public void OutScoreToGameOverScreen()
    {
        resultScoreText.text = Convert.ToString(score);
        resultLinesText.text = String.Format("You cleared {0} lines", lines);
    }

    public void CheckCombo(int numberOfLines)
    {
        switch (numberOfLines)
        {
            case 1:
                AddPoints(scoreOneLine);
                break;

            case 2:
                AddPoints(scoreTwoLine);
                break;

            case 3:
                AddPoints(scoreThreeLine);
                break;

            case 4:
                AddPoints(scoreFourLine);
                uiController.PlayTetris();
                break;
                
            default:
                break;
        }
        
    }
    private void AddPoints(int points)
    {
        score += points;
        OutScoreToHud();
    }

    public static void GetResult()
    {
        PlayerStatic.BestScore = score;
        PlayerStatic.AllLineCleared = lines;
    }
}
