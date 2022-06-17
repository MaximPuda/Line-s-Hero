using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ScoreSystem scoreSystem;
    [SerializeField] private UIController uiController;
    [SerializeField] private AudioController musicPlayer;
    [SerializeField] private CameraMove cameraMove;
    [SerializeField] private CameraAnimation cameraAnimation;

    [SerializeField] private UnityEvent onStartGame;
    [SerializeField] private UnityEvent onGameOver;

    public static bool isGamePaused;
    public static bool isGameEnded;

    private void Start()
    {
        PreStartGame();
    }
    public void PreStartGame()
    {
        uiController.ActivateHud(false);
        uiController.ActivatePreStart(true);
        isGameEnded = true;
        cameraMove.enabled = false;
        cameraAnimation.PlayPreStartAnimation();
    }

    public void StartGame()
    {
        uiController.ActivateHud(true);
        uiController.ActivatePreStart(false);
        cameraMove.enabled = true;
        isGameEnded = false;
        isGamePaused = false;
        onStartGame.Invoke();
    }
    public void Restart()
    {
        StartGame();
        SceneManager.LoadScene("PlayGame");
        Time.timeScale = 1;
    }

    public void GameOver()
    {
        cameraMove.enabled = false;
        onGameOver.Invoke();
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
