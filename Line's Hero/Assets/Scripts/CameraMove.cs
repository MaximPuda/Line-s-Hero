using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform mainCamera;
    public float speed = 10;
    public Transform lookAt;

    void Update()
    {
        Vector3 tempPos = Vector3.Lerp(mainCamera.position, new Vector3(Spawner2.activeBlock.transform.position.x,
            mainCamera.position.y, mainCamera.position.z), speed * Time.deltaTime);
        mainCamera.position = tempPos;
        mainCamera.LookAt(lookAt);

    }
}
