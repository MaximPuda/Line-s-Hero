using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject sceneTransition;
    [SerializeField] private GameManager gameManager;
   
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void StartTransition(string sceneName)
    {
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
        Time.timeScale = 1;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}
