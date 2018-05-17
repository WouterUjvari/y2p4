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
		print ("button click");
        if (canInteract)
        {
            interactEvent.Invoke();

            anim.SetTrigger("Interact");
            canInteract = false;
        }
    }

    public override void DeInteract(VRInteractor hand)
    {

    }

    public void ResetCanInteract()
    {
        canInteract = true;
    }
}
