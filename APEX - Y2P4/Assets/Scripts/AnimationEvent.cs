using UnityEngine;
using UnityEngine.Events;

public class AnimationEvent : MonoBehaviour 
{

    [SerializeField]
    private UnityEvent onAnimationEvent;

    public void CallAnimationEvent()
    {
        onAnimationEvent.Invoke();
    }
}
