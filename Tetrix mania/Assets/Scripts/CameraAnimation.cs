using System.Collections;
using UnityEngine;

public class CameraAnimation : MonoBehaviour
{
    [Header("Pre Start Animation")]
    [SerializeField] private float preStartZ;
    [SerializeField, Range(0, 1)] private float speed = 0.05f;

    [Header("Camera Shake Animation")]
    [SerializeField] private float duration = 0.3f;
    [SerializeField] private float magnitude = 0.3f;
    [SerializeField, Range(0, 1)] private float shakeSpeed;

    private Vector3 endPosition;

    private IEnumerator PreStartAnimation()
    {
        endPosition = transform.position;

        Vector3 tempPos = new Vector3(transform.position.x, transform.position.y, preStartZ);
        while (endPosition.z - tempPos.z > 0.01f)
        {
            tempPos = Vector3.Lerp(tempPos, endPosition, speed);
            transform.position = tempPos;
            yield return null;
        }

        transform.position = endPosition;
    }

    private IEnumerator CameraShakeAnimation()
    {
        endPosition = transform.position;

        float elapsed = 0f;
        //Vector3 tempPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        while (elapsed < duration)
        {
            float tempX = Random.Range(-1, 1) * magnitude;
            transform.position = new Vector3(tempX, endPosition.y, endPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.position = endPosition;
    }

    public void PlayPreStartAnimation()
    {
        StartCoroutine(PreStartAnimation());
    }
    
    public void PlayCameraShake()
    {
        StartCoroutine(CameraShakeAnimation());
    }
}
