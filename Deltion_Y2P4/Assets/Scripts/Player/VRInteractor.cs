using UnityEngine;

public class VRInteractor : MonoBehaviour
{

    private SteamVR_TrackedObject trackedObj;

    private GameObject collidingObject;
    private Interactable interactingObject;

    [SerializeField]
    private VRInteractor otherHand;
    private HandActions handActions;

    [HideInInspector]
    public SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    private void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        handActions = GetComponentInChildren<HandActions>();
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

        Vector2 triggerAxis = Controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger);
        handActions.timeline = triggerAxis.x;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (collidingObject != null || !other.GetComponent<Rigidbody>())
        {
            return;
        }

        if (otherHand != null)
        {
			if (otherHand.interactingObject == null || otherHand.interactingObject.gameObject != collidingObject)
            {
                Highlightable highlightable = other.GetComponent<Highlightable>();
                if (highlightable != null)
                {
					print ("test");
                    highlightable.Highlight();
                }            
			}
        }

        Interactable interactable = other.GetComponent<Interactable>();
        if (interactable != null)
        {
            handActions.press = (interactable is Clickable ? true : false);
        }

        collidingObject = other.gameObject;
    }

    public void OnTriggerExit(Collider other)
    {
        if (collidingObject == null)
        {
            return;
        }

        Highlightable highlightable = other.GetComponent<Highlightable>();
        if (highlightable != null)
        {
            highlightable.DeHighlight();
        }

        Interactable interactable = other.GetComponent<Interactable>();
        if (interactable != null)
        {
            handActions.press = false;
        }

        collidingObject = null;
    }

    private void Interact()
    {
        if (otherHand != null)
        {
			if (otherHand.interactingObject != null && otherHand.interactingObject.gameObject == collidingObject)
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
			print ("interactable found");
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
