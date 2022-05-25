using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailHandler : MonoBehaviour
{
    [SerializeField] private ParticleSystem particles;
    
    private Transform activeBlock;
    private TrailRenderer trail;

    private Vector3 offset;

    private void Awake()
    {
        trail = GetComponent<TrailRenderer>();
    }

    void Update()
    {
        transform.position = activeBlock.position + offset;
        particles.gameObject.transform.position = activeBlock.position + offset;
    }

    public void SetActiveBlock(Transform blockTransform)
    {
        activeBlock = blockTransform;
        trail.Clear();
    }

    public void EmissionEnable()
    {
        SetTrailWidthAndOffset();
        trail.emitting = true;
        particles.Play();
    }

    public void EmissionDesable()
    {
        trail.emitting = false;
        particles.Stop();
    }

    private void SetTrailWidthAndOffset()
    {
        float minX = float.MaxValue;
        float maxX = float.MinValue;
        float minY = float.MaxValue;
        float maxY = float.MinValue;
        var allYPos = new List<float>();
        int countSameMinY = 0;
        int countSameMaxY = 0;

        for (int i = 0; i < activeBlock.childCount; i++)
        {
            if (activeBlock.GetChild(i).tag == "Cube")
            {
                var childX = activeBlock.GetChild(i).transform.position.x;
                var childY = activeBlock.GetChild(i).transform.position.y;
                
                if (childX < minX)
                    minX = childX;
                
                if (childX > maxX)
                    maxX = childX;
              
                if (childY > maxY)
                    maxY = childY;
                
                if (childY < minY)
                    minY = childY;

                allYPos.Add(childY);
            }
        }

        for (int i = 0; i < allYPos.Count; i++)
        {
            if (Mathf.RoundToInt(allYPos[i] - minY) == 0)
                countSameMinY++;

            if (Mathf.RoundToInt(allYPos[i] - maxY) == 0)
                countSameMaxY++;
        }

        float widthX = maxX - minX + 1;
        float widthY = maxY - minY + 1;
        float centerX = (minX + (widthX - 1) / 2);
        float centerY = (minY + (widthY - 1) / 2);
        float offsetX = centerX - activeBlock.transform.position.x;
        float offsetY = centerY - activeBlock.transform.position.y;

        if (countSameMinY > 1 && countSameMinY != countSameMaxY)
            offsetY -= 1f;
        else if (countSameMaxY > 1 && countSameMinY != countSameMaxY)
            offsetY += 1f;

            offset = new Vector3(offsetX, offsetY, 0);

        transform.localScale = new Vector3(widthX, 1, 1);
        particles.shape.scale.Set(widthX - 0.5f, 1, 1);
    }
}
