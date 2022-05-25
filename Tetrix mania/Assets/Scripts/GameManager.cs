using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ScoreSystem scoreSystem;
    [SerializeField] private UIController uiController;
    [SerializeField] private AudioController musicPlayer;

    public static bool isGamePaused;
    public static bool isGameEnded;

    public static void StartGame()
    {
        isGameEnded = false;
        isGamePaused = false;
    }
    public void Restart()
    {
        StartGame();
        SceneManager.LoadScene("PlayGame");
        Time.timeScale = 1;
    }

    public void GameOver()
    {
        musicPlayer.StopMusic();
        uiController.PlayGameOverOpen();
        isGameEnded = true;
        scoreSystem.GetFinalResult();
    }

    public void Pause()
    {
        if (!isGamePaused)
        {
            Time.timeScale = 0;
            musicPlayer.PauseMusic();
            isGamePaused = true;
            uiController.PlayPauseOpen();
        }
        else
        {
            Time.timeScale = 1;
            musicPlayer.PlayMusic();
            isGamePaused = false;
            uiController.PlayPauseClose();
        }
    }
}
