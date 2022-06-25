using System.Collections;
using UnityEngine;

public class CameraAnimation : MonoBehaviour
{
    [SerializeField] private CameraMove cameraMove;
    [Header("Pre Start Animation")]
    [SerializeField] private float preStartZ;
    [SerializeField, Range(0, 1)] private float speed = 0.05f;

    [Header("Camera Shake Animation")]
    [SerializeField] private float duration = 1;
    [SerializeField] private float magnitude = 0.3f;

    private IEnumerator MoveTo(Vector3 start, Vector3 target, float speed)
    {
        Vector3 tempPos = start;
        cameraMove.enabled = false;

        while (Vector3.Distance(tempPos, target) > 0.01f)
        {
            tempPos = Vector3.Lerp(tempPos, target, speed);
            transform.position = tempPos;
            yield return null;
        }

        transform.position = target;
        cameraMove.enabled = true;
    }

    private IEnumerator CameraShakeAnimation()
    {
        var endPosition = transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float tempX = Random.Range((endPosition.x - magnitude), endPosition.x + magnitude);
            float tempY = Random.Range((endPosition.y - magnitude), endPosition.y + magnitude);
            transform.position = new Vector3(tempX, tempY, endPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.position = endPosition;
    }

    public void PlayPreStartAnimation()
    {
        var start = new Vector3(transform.position.x, transform.position.y, preStartZ);
        var end = transform.position;
        StartCoroutine(MoveTo(start, end, speed));
    }
    
    public void PlayCameraShake()
    {
        StartCoroutine(CameraShakeAnimation());
    }
}
