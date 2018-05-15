using UnityEngine;

public abstract class Interactable : MonoBehaviour
{

    public abstract void Interact(VRInteractor hand);
    public abstract void DeInteract(VRInteractor hand);
}
