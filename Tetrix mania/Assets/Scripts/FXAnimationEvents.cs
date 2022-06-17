using UnityEngine;
using UnityEngine.Events;

public class FXAnimationEvents : MonoBehaviour
{
    [SerializeField] private UnityEvent onEndAnimation;

    public void AnimationEnd()
    {
        onEndAnimation.Invoke();
    }
}
