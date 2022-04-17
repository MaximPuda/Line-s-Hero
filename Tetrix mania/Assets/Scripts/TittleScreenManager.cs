using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TittleScreenManager : MonoBehaviour
{
    public TextMeshProUGUI playerNameLabel;
    public TMP_InputField newPlayerName;

    public Animator welcomeAnimation;

    void Start()
    {
        if (PlayerStatic.LoadPlayer())
        {
            welcomeAnimation.SetTrigger("Welcome");
            playerNameLabel.text = PlayerStatic.PlayerName;
            welcomeAnimation.SetBool("IsBlinking", true);
        }
        else
        {
            welcomeAnimation.SetTrigger("CreateNewPlayer");
        }
    }

    public void CreateNewPlayer()
    {
        PlayerStatic.PlayerName = newPlayerName.text;
        PlayerStatic.SavePlayer();
        playerNameLabel.text = PlayerStatic.PlayerName;
        welcomeAnimation.SetTrigger("Welcome");
        welcomeAnimation.SetBool("IsBlinking", true);
    }
}
