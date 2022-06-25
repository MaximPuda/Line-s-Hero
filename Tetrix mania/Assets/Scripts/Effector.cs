using UnityEngine;
using System.Collections;

public class Effector : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] lineClearedParticles;
    [SerializeField] private ParticleSystem ComboParticles;
    [SerializeField] private ParticleSystem speedUpParticles;

    [Header("CupNeonLight")]
    [SerializeField] private SpriteRenderer cupNeonLIghtRendrer;
    [SerializeField] private float step = 0.1f;
    [SerializeField] private float delay = 0.03f;

    private bool isCupNeonLightRuning = false;

    public void PlayLineClearedParticles(float lineY)
    {
        if(!isCupNeonLightRuning)
            StartCoroutine(PlayCupLight());

        for (int i = 0; i < lineClearedParticles.Length; i++)
            if (!lineClearedParticles[i].isPlaying)
            {
                lineClearedParticles[i].transform.position = new Vector3(4.5f, lineY, -1f);
                lineClearedParticles[i].Play();
                break;
            }
    }

    public void PlayComboParticles()
    {
        ComboParticles.Play();
    }

    public void PlayFXPartiles(string message)
    {
        if(message == "SPEED UP")
        {
            speedUpParticles.Play();
        }
    }

     IEnumerator PlayCupLight()
    {
        isCupNeonLightRuning = true;
        float value = 0f;
        Color startColor = cupNeonLIghtRendrer.color;
        while (value < 1)
        {
            value += step; 
            cupNeonLIghtRendrer.color = new Color(startColor.a, startColor.g, startColor.b, value);
            yield return new WaitForSeconds(delay);
        }

        while (value > 0)
        {
            value -= step;
            cupNeonLIghtRendrer.color = new Color(startColor.a, startColor.g, startColor.b, value);
            yield return new WaitForSeconds(delay);
        }

        cupNeonLIghtRendrer.color = startColor;
        isCupNeonLightRuning = false;
    }
}
