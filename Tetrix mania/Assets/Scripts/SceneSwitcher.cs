using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject sceneTransition;
   
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
    }

    public void LoadStartGameScene()
    {
        ADSBanner.instance.HideBannerAd();
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void LoadMainMenu()
    {
        ADSBanner.instance.ShowBannerAd();
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
