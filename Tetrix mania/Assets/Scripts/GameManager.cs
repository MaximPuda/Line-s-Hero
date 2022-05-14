using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ScoreSystem scoreSystem;
    [SerializeField] private UIController uiController;
    [SerializeField] private Timer timer;

    public static bool gamePaused;
    public static bool gameEnded;

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
