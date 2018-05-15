using UnityEngine;

public class VRInteractor : MonoBehaviour
{

    private SteamVR_TrackedObject trackedObj;

    private GameObject collidingObject;
    private Interactable interactingObject;

	[SerializeField]
	private VRInteractor otherHand;

    [HideInInspector]
    public SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    private void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    private void Update()
    {
        if (Controller.GetHairTriggerDown())
        {
            if (collidingObject != null)
            {
                Interact();
            }
        }

        if (Controller.GetHairTriggerUp())
        {
            if (interactingObject != null)
            {
                DeInteract();
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (collidingObject != null || !other.GetComponent<Rigidbody>())
        {
            return;
        }

        if (otherHand != null)
        {
            if (otherHand.interactingObject.gameObject != collidingObject)
            {
                Highlightable highlightable = other.GetComponent<Highlightable>();
                if (highlightable != null)
                {
                    highlightable.Highlight();
                }
            }
        }


        collidingObject = other.gameObject;
    }

    public void OnTriggerExit(Collider other)
    {
        if (collidingObject == null)
        {
            return;
        }

        Highlightable interactable = other.GetComponent<Highlightable>();
        if (interactable != null)
        {
            interactable.DeHighlight();
        }

        collidingObject = null;
    }

    private void Interact()
    {
		if (otherHand != null) 
		{
			if (otherHand.interactingObject.gameObject == collidingObject) 
			{
				otherHand.DeInteract();
			}
		}

        Highlightable highlightable = collidingObject.GetComponent<Highlightable>();
        if (highlightable != null)
        {
            highlightable.DeHighlight();
        }

        Interactable interactable = collidingObject.GetComponent<Interactable>();
        if (interactable != null)
        {
            interactable.Interact(this);
            interactingObject = interactable;
            collidingObject = null;
        }
    }

    public void DeInteract()
    {
        interactingObject.DeInteract(this);
        interactingObject = null;
    }
}
