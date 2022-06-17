using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIController : MonoBehaviour
{
    [Header ("Animators")]
    [SerializeField] private Animator pauseAnimator;
    [SerializeField] private Animator gameOverAnimator;
    [SerializeField] private Animator fxAnimator;
    //[SerializeField] private Animator messageAnimator;
    //[SerializeField] private Animation messageAnimation;

    [Header("References")]
    [SerializeField] private Canvas hudCanvas;
    [SerializeField] private Canvas preStart;
    [SerializeField] private Toggle musicToggle;
    [SerializeField] private Toggle soundsToggle;
    [SerializeField] private TMP_Text message;
    [SerializeField] private TMP_Text messageBack;

    [SerializeField] private UnityEvent<string> onFxPlay;


    private List<string> messages;
    private bool isAnimationEnd;

    private void Awake()
    {
        messages = new List<string>();
        isAnimationEnd = true;
    }

    private void Update()
    {
        if (messages.Count > 0 && isAnimationEnd && fxAnimator.GetCurrentAnimatorStateInfo(0).IsName("Default"))
        {
            message.text = messages[0];
            messageBack.text = messages[0];

            fxAnimator.SetTrigger(messages[0]);
            onFxPlay.Invoke(messages[0]);
            
            isAnimationEnd = false;
            messages.RemoveAt(0);
        }
    }

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

    public void PlayFX(string message)
    {
        if (message == "TETRIX")
            messages.Insert(0, message);
        else
            messages.Add(message);
    }

    public void SetAnimationEnd()
    {
        isAnimationEnd = true;
    }

    public void ActivateHud(bool isActive)
    {
        hudCanvas.enabled = isActive;
    }
    public void ActivatePreStart(bool isActive)
    {
        preStart.enabled = isActive;
    }
}
