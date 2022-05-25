using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    void Start()
    {
        if (!Player.Load())
            Player.Reset();
    }

    public void DeleteSaveFile()
    {
        FileManager.DeleteSaveFile();
    }
}
