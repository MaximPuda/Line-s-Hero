using System;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private int scoreOneLine = 100;
    [SerializeField] private int scoreTwoLine = 300;
    [SerializeField] private int scoreThreeLine = 700;
    [SerializeField] private int scoreFourLine = 1500;

    public TextMeshProUGUI linesText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI resultLinesText;
    public TextMeshProUGUI resultScoreText;
    public UIController uiController;

    [SerializeField] private UnityEvent<int> OnAddLines;
    [SerializeField] private UnityEvent OnBestScore;
    [SerializeField] private UnityEvent OnTetrix;

    private int lines = 0;
    private int score = 0;
    private int bestScore = 0;
    private bool isBest = false;

    private  void Start()
    {
        bestScore = Player.GetBestScore(GameModeSettings.mode);
        OutScoreToHud();
    }

    private void AddPoints(int points)
    {
        score += points;
        CheckBestScore();
        OutScoreToHud();
    }

    private void OutScoreToHud()
    {
        linesText.text = lines.ToString();
        scoreText.text = score.ToString();
        bestScoreText.text = bestScore.ToString();
    }

    public void AddLines()
    {
        lines++;
        OutScoreToHud();
        OnAddLines.Invoke(lines);
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
                OnTetrix.Invoke();
                break;
                
            default:
                break;
        }
        
    }

    public void CheckBestScore()
    {
        if(score > bestScore)
        {
            bestScore = score;

            if (!isBest)
            {
                isBest = true;
                OnBestScore.Invoke();
            }
        }
    }

    public void GetFinalResult()
    {
        resultScoreText.text = score.ToString();
        resultLinesText.text = String.Format("You cleared {0} lines", lines);
        Player.SetBestScore(GameModeSettings.mode, bestScore);
        Player.AddLines(lines);
    }
}
