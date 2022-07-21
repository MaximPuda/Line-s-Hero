using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    void Start()
    {
        if (!Player.Load())
            Player.Reset();
    }

    public void DeleteSaveFile()
    {
        Player.Reset();
    }
}
