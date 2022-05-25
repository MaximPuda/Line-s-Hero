using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    [Header("Music")]
    [SerializeField] private AudioSource musicPlayer;
    [SerializeField] private AudioClip[] music;
    [SerializeField] private ParticleSystem visualizer;
    [SerializeField] private bool musicVisualization;
    [SerializeField, Range(64, 1024)] private int sampleRate = 64;

    [Header("UI sounds")]
    [SerializeField] private AudioSource buttonClick;
    [SerializeField] private AudioSource togleClick;

    [Header("FX sounds")]
    [SerializeField] private AudioSource actionPlayer;
    [SerializeField] private AudioClip[] sounds;
    [SerializeField] private float maxPitch = 1.1f;

    private float currentTime;
    float[] spectrumData;

    private void Start()
    {
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
            visualizer.Stop();
            visualizer.startSpeed = peak * 5;
            visualizer.startLifetime = peak * 5;
            visualizer.Emit(50);

            currentTime = Time.time;
        }
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
        actionPlayer.clip = null;
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == name)
                actionPlayer.clip = sounds[i];
        }

        if (actionPlayer.clip != null)
        {
            actionPlayer.pitch = Random.Range(1, maxPitch);
            actionPlayer.Play();
        }
        else
            Debug.Log($"Sound {name} not found!");
    }    
}
