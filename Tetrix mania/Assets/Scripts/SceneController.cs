using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneController : MonoBehaviour
{
    public GameObject sceneTransition;
    private Animator animator;
    public void StartTransition(string sceneName)
    {
        animator = GetComponent<Animator>();
        switch (sceneName)
        {
            case "PlayGame":
                animator.SetTrigger("PlayGame");
                break;

            case "MainMenu":
                animator.SetTrigger("MainMenu");
                break;

            default:
                break;
        }
        Time.timeScale = 1;
    }

    public void LoadStartGameScene()
    {
        SceneManager.LoadScene(1);
        GameManager.StartGame();
        Time.timeScale = 1;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}
