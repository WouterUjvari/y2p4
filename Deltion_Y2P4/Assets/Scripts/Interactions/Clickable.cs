using UnityEngine;
using UnityEngine.Events;

public class Clickable : Interactable
{

    [SerializeField]
    private UnityEvent interactEvent;

    private bool canInteract = true;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public override void Interact(VRInteractor hand)
    {
        base.Interact(hand);

        if (canInteract)
        {
            interactEvent.Invoke();

            anim.SetTrigger("Interact");
            canInteract = false;
        }
    }

    public override void DeInteract(VRInteractor hand)
    {
        base.DeInteract(hand);
    }

    public void ResetCanInteract()
    {
        canInteract = true;
    }
}
