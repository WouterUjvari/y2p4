using UnityEngine;
using UnityEngine.Events;

public abstract class Interactable : MonoBehaviour
{

    [SerializeField]
    private UnityEvent onInteract;
    [SerializeField]
    private UnityEvent onDeInteract;

    public virtual void Interact(VRInteractor hand)
    {
        onInteract.Invoke();
    }

    public virtual void DeInteract(VRInteractor hand)
    {
        onDeInteract.Invoke();
    }
}
