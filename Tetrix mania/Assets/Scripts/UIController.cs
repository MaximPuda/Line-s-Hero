using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private Animator pauseAnimator;
    [SerializeField] private Animator gameOverAnimator;
    [SerializeField] private Animator fxAnimator;

    [SerializeField] private Canvas hudCanvas;

    [Header("Messsages")]
    [SerializeField] private Animator messageAnimator;
    [SerializeField] private TMP_Text message;
    [SerializeField] private TMP_Text messageBack;

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

    public void ShowMessage(string message)
    {
        this.message.text = message;
        messageBack.text = message;

        messageAnimator.SetTrigger("ShowMessage");
    }
}
