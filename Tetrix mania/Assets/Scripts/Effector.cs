using UnityEngine;

public class Effector : MonoBehaviour
{
    public ParticleSystem[] lineClearedParticles;

    public void PlayLineClearedParticles(float lineY)
    {
        for (int i = 0; i < lineClearedParticles.Length; i++)
            if (!lineClearedParticles[i].isPlaying)
            {
                lineClearedParticles[i].transform.position = new Vector3(4.5f, lineY, -1f);
                lineClearedParticles[i].Play();
                break;
            }
    }
}
