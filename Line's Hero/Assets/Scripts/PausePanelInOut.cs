using UnityEngine;

public class PausePanelInOut : MonoBehaviour
{
    public void Continue()
    {
        FindObjectOfType<GameManager>().Pause();
    }
}
