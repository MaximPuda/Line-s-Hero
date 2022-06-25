using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ScoreSystem scoreSystem;
    [SerializeField] private UIController uiController;
    [SerializeField] private AudioController audioController;
    [SerializeField] private CameraMove cameraMove;
    [SerializeField] private CameraAnimation cameraAnimation;

    [SerializeField] private UnityEvent onGameStart;

    public static bool isGamePaused;
    public static bool isGameEnded;

    private void Start()
    {
        PreStartGame();
    }
    public void PreStartGame()
    {
        audioController.SetAndPlayMusic(GameModeSettings.bgMusic);
        uiController.ActivateHud(false);
        uiController.ActivatePreStart(true);
        isGameEnded = true;
        cameraAnimation.PlayPreStartAnimation();
    }

    public void StartGame()
    {
        onGameStart.Invoke();
        audioController.PlayFXSounds("GameStart");
        uiController.ActivateHud(true);
        uiController.ActivatePreStart(false);
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
        audioController.PlayFXSounds("GameOver");
        cameraMove.enabled = false;
        cameraAnimation.PlayCameraShake();
        audioController.StopMusic();
        uiController.PlayGameOverOpen();
        isGameEnded = true;
        scoreSystem.GetFinalResult();
    }

    public void Pause()
    {
        if (!isGamePaused)
        {
            Time.timeScale = 0;
            audioController.PauseMusic();
            isGamePaused = true;
            uiController.PlayPauseOpen();
        }
        else
        {
            Time.timeScale = 1;
            audioController.PlayMusic();
            isGamePaused = false;
            uiController.PlayPauseClose();
        }
    }
}
