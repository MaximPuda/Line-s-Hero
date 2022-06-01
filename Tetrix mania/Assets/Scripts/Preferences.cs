using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Preferences : MonoBehaviour
{
    [SerializeField] private Toggle musicToggle;
    [SerializeField] private Toggle soundsToggle;
    [SerializeField] private Slider sensSlider;
    [SerializeField] private TMP_InputField playerName;

    public void loadToUI()
    {
        musicToggle.isOn = PlayerPrefs.GetFloat("Music") > -70f;
        soundsToggle.isOn = PlayerPrefs.GetFloat("Sounds") > -70f;
        sensSlider.value = PlayerPrefs.GetFloat("Sens");

        if(playerName != null)
            playerName.text = Player.playerName;
    }

    public void SaveSenseToPrefs(float value)
    {
        PlayerPrefs.SetFloat("Sens", value);
    }

    public void ChangePlayerName(string name)
    {
        Player.SetPlayerName(name);
    }    
}
