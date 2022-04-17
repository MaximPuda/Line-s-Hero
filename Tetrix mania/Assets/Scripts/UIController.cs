using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private Animator pauseAnimator;
    [SerializeField] private Animator gameOverAnimator;
    [SerializeField] private Animator fxAnimator;

    [SerializeField] private Canvas hudCanvas;

    public void PlayPauseOpen()
    {
        pauseAnimator.SetTrigger("PauseOpen");
        hudCanvas.enabled = false;
    }

    public void PlayPauseClose()
    {
        pauseAnimator.SetTrigger("PauseClose");
        hudCanvas.enabled = true;
    }

    public void PlayGameOverOpen()
    {
        gameOverAnimator.SetTrigger("GameOver");
    }

    public void PlayTetris()
    {
        fxAnimator.SetTrigger("Tetris");
    }
}
