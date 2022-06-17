using UnityEngine;
using UnityEngine.Events;

public class UIEvents : MonoBehaviour
{
    [SerializeField] private UnityEvent openWindow;
    [SerializeField] private UnityEvent closeWindow;
    [SerializeField] private UnityEvent warning;

    public void OpenWindow()
    {
        openWindow.Invoke();
    }
    public void CloseWindow()
    {
        closeWindow.Invoke();
    }

    public void Warning()
    {
        warning.Invoke();
    }

}
