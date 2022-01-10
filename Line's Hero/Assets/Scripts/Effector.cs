using UnityEngine;

public class Effector : MonoBehaviour
{
    public ParticleSystem lineClearedParticles;
    public ParticleSystem newParticles;

    public void PlayLineCleardParticles(float lineY)
    {
        newParticles = Instantiate(lineClearedParticles);
        newParticles.transform.position = new Vector3(4.5f, lineY, -1f);
    }
}
