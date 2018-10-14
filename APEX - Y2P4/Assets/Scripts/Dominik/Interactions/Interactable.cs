using UnityEngine;
using UnityEngine.Events;

public abstract class Interactable : MonoBehaviour
{

    protected bool locked;
    public bool Locked
    {
        get { return locked; }
    }

    [SerializeField] public UnityEvent onInteract;
    [SerializeField] public UnityEvent onDeInteract;

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
