using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hint;
    [SerializeField] private TextMeshProUGUI number;
    [SerializeField] private Animator animator;
    [SerializeField] private string[] hints;

    private int hintNumber = 0;

    private void ShowHint(int index)
    {
        hint.text = hints[index];
        number.text = (hintNumber + 1) + "/" + hints.Length; 
    }

    public void Next()
    {
        if (hintNumber < hints.Length)
        {
            animator.SetTrigger("Next");
            ShowHint(hintNumber);
        }
        else
        {
            hintNumber = 0;
            gameObject.SetActive(false);
            Time.timeScale = 1;
            GameManager.isGamePaused = false;
        }

        hintNumber++;
    }

    public void StartTuttorial()
    {
        var isFirstRun = PlayerPrefs.GetInt("isFirstRun");
        if(isFirstRun == 0)
        {
            PlayerPrefs.SetInt("isFirstRun", 1);
            Time.timeScale = 0;
            GameManager.isGamePaused = true;
            hintNumber = 0;
            gameObject.SetActive(true);
            Next();
        }
    }

    public void ShowTutorialAgain()
    {
        PlayerPrefs.SetInt("isFirstRun", 0);
        StartTuttorial();
    }
}
