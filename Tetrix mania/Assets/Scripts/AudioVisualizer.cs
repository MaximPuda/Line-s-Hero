using UnityEngine;

public class AudioVisualizer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioSource musicPlayer;
    [SerializeField] private ParticleSystem[] visualizers;

    [Header("Settings")]
    [SerializeField] private bool musicVisualization;
    [SerializeField, Range(64, 1024)] private int sampleRate = 64;
    [SerializeField] private float sizeAmplitude = 10;
    [SerializeField] private int particlesEmitCount = 50;

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
}
