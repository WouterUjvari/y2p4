using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class Interactable : MonoBehaviour
{

    // Lock can be used for locking interactables when
    // for example they are placed in a closed cupboard.
    private bool locked;
    public bool Lock
    {
        get
        {
            return locked;
        }
        set
        {
            locked = value;
            OnLockInteractable(locked);
        }
    }

    public UnityEvent OnInteract;
    public UnityEvent OnDeInteract;

    public event Action<bool> OnLockInteractable = delegate { };

    protected VRInteractor interactingHand;

    public virtual void Interact(VRInteractor hand)
    {
        OnInteract.Invoke();
    }

    public virtual void DeInteract(VRInteractor hand)
    {
        OnDeInteract.Invoke();
    }
}
