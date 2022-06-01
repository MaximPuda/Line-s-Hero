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
    [SerializeField] private AudioClip[] music;

    [Header("Visualizer")]
    [SerializeField] private ParticleSystem[] visualizers;
    [SerializeField] private bool musicVisualization;
    [SerializeField, Range(64, 1024)] private int sampleRate = 64;
    [SerializeField] private float sizeAmplitude = 10;
    [SerializeField] private int particlesEmitCount = 50;

    [Header("UI sounds")]
    [SerializeField] private AudioSource uiPlayer;
    [SerializeField] private AudioClip[] uiSounds;

    [Header("Action sounds")]
    [SerializeField] private AudioSource actionPlayer;
    [SerializeField] private AudioClip[] actionSounds;
    [SerializeField] private float maxPitch = 1.1f;

    [Header("FX sounds")]
    [SerializeField] private AudioSource fxPlayer;
    [SerializeField] private AudioClip[] fxSounds;

    private float currentTime;
    float[] spectrumData;

    private void Start()
    {
        mixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("Music"));
        mixer.SetFloat("SoundsVolume", PlayerPrefs.GetFloat("Sounds"));

        currentTime = Time.time;
        spectrumData = new float[sampleRate];
    }

    private void Update()
    {
        if (musicVisualization && Time.time - currentTime >= 0.1f)
        {
            float peak = 0;
            musicPlayer.GetSpectrumData(spectrumData, 0, FFTWindow.Rectangular);
            for (int i = 0; i < spectrumData.Length; i++)
            {
                peak += spectrumData[i];
            }
            for (int i = 0; i < visualizers.Length; i++)
            {
                visualizers[i].Stop();
                visualizers[i].startSpeed = peak * sizeAmplitude;
                visualizers[i].startLifetime = peak;
                visualizers[i].Emit(particlesEmitCount);
            }

            currentTime = Time.time;
        }
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
