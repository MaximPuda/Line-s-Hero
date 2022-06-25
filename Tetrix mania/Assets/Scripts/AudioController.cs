using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    [Header("Mixer")]
    [SerializeField] private AudioMixer mixer;

    [Header("Music")]
    [SerializeField] private AudioSource musicPlayer;
    [SerializeField] private AudioClip mainMenuMusic;

    [Header("UI sounds")]
    [SerializeField] private AudioSource uiPlayer;
    [SerializeField] private AudioClip[] uiSounds;

    [Header("Action sounds")]
    [SerializeField] private AudioSource actionPlayer;
    [SerializeField] private AudioClip[] actionSounds;

    [Header("FX sounds")]
    [SerializeField] private AudioSource fxPlayer;
    [SerializeField] private AudioClip[] fxSounds;

    private void Start()
    {
        mixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("Music"));
        mixer.SetFloat("SoundsVolume", PlayerPrefs.GetFloat("Sounds"));
    }

    private AudioClip FindClip(AudioClip[] sounds, string name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == name)
                return sounds[i];
        }

        Debug.Log($"Sound {name} not found!");
        return null;
    }

    public void SetAndPlayMusic(AudioClip clip)
    {
        musicPlayer.clip = clip;
        musicPlayer.Play();
    }

    public void PlayMusic()
    {
        musicPlayer.Play();
    }

    public void StopMusic()
    {
        musicPlayer.Stop();
    }

    public void PauseMusic()
    {
        musicPlayer.Pause();
    }

    public void PlayActionSound(string name)
    {
        actionPlayer.clip = FindClip(actionSounds, name);

        if (actionPlayer.clip != null)
            actionPlayer.Play();
    }

    public void PlayUISounds(string name)
    {
        uiPlayer.clip = FindClip(uiSounds, name);

        if (uiPlayer.clip != null)
            uiPlayer.Play();
    }

    public void PlayFXSounds(string name)
    {
        fxPlayer.clip = FindClip(fxSounds, name);

        if (fxPlayer.clip != null)
            fxPlayer.Play();
    }

    public void MuteMusic(bool isActive)
    {
        if (!isActive)
        {
            mixer.SetFloat("MusicVolume", -80f);
            PlayerPrefs.SetFloat("Music", -80f);
        }
        else
        {
            mixer.SetFloat("MusicVolume", 0f);
            PlayerPrefs.SetFloat("Music", 0f);
        }    

    }

    public void MuteSounds(bool isActive)
    {
        if (!isActive)
        {
            mixer.SetFloat("SoundsVolume", -80f);
            PlayerPrefs.SetFloat("Sounds", -80f);
        }
        else
        {
            mixer.SetFloat("SoundsVolume", 0f);
            PlayerPrefs.SetFloat("Sounds", 0f);
        }
    }
}
