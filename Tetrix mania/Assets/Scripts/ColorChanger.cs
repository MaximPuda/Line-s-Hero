using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private MeshRenderer borders;
    [SerializeField] private MeshRenderer board;
    [SerializeField] private ParticleSystem[] particles;
    [SerializeField] private Material[] skyboxes;

    [Header("Colors")]
    [SerializeField] private Color[] colors;

    public void ChangeColor()
    {
        var targetColor = colors[Random.Range(0, colors.Length)];
        while(targetColor == borders.material.color)
            targetColor = colors[Random.Range(0, colors.Length)];

        borders.material.color = targetColor;
        board.material.color = new Color(targetColor.r - 0.2f, targetColor.g - 0.2f, targetColor.b -0.2f);

        targetColor= new Color(targetColor.r + 0.2f, targetColor.g + 0.2f, targetColor.b + 0.2f, 1f);
        for (int i = 0; i < particles.Length; i++)
        {
            particles[i].startColor = targetColor;
        }
    }
}
