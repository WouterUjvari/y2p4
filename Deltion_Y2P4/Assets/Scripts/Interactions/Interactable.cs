using UnityEngine;
using UnityEngine.Events;

public abstract class Interactable : MonoBehaviour
{

    protected bool locked;

    [SerializeField]
    private UnityEvent onInteract;
    [SerializeField]
    private UnityEvent onDeInteract;

    protected VRInteractor interactingHand;

    public virtual void Interact(VRInteractor hand)
    {
        onInteract.Invoke();
    }

    public virtual void DeInteract(VRInteractor hand)
    {
        onDeInteract.Invoke();
    }

    public void Lock(bool b)
    {
        locked = b;
    }
}
