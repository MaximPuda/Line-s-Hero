using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Transform mainCamera;
    [SerializeField] private float speed = 10;
    [SerializeField] private Transform lookAt;
    [SerializeField] private BlockController blockController;

    private Transform activeBlock;

    void Update()
    {
        if (!GameManager.isGameEnded && !GameManager.isGamePaused)
        {
            activeBlock = blockController.GetActiveBlock();

            Vector3 tempPos = Vector3.Lerp(mainCamera.position, new Vector3(activeBlock.position.x,
                mainCamera.position.y, mainCamera.position.z), speed * Time.deltaTime);
            mainCamera.position = tempPos;
            mainCamera.LookAt(lookAt);
        }
    }
}
