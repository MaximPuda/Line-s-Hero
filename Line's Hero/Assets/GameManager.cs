using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public ScoreSystem scoreSystem;

    public static bool gamePaused;
    public static bool gameEnded;

    public UIController uiController;

    public void GameOver()
    {
        uiController.PlayGameOverOpen();
        gameEnded = true;
        scoreSystem.OutScoreToGameOverScreen();
        ScoreSystem.GetResult();
        PlayerStatic.SavePlayer();
    }

    public void Pause()
    {
        if (!gamePaused)
        {
            Time.timeScale = 0;
            gamePaused = true;
            uiController.PlayPauseOpen();
        }
        else
        {
            Time.timeScale = 1;
            gamePaused = false;
            uiController.PlayPauseClose();
        }
    }

    public void Restart()
    {
        StartGame();
        SceneManager.LoadScene("PlayGame");
        Time.timeScale = 1;
    }

    public static void StartGame()
    {
        gameEnded = false;
        gamePaused = false;
        ScoreSystem.ResetScore();
    }
}
