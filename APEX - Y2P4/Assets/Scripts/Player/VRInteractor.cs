using UnityEngine;

public class VRInteractor : MonoBehaviour
{

    private SteamVR_TrackedObject trackedObj;

    public GameObject collidingObject;
    public Interactable interactingObject;

    [SerializeField]
    private VRInteractor otherHand;
    private HandActions handActions;

#if !TEST
    [HideInInspector]
    public SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }
#endif

#if TEST
    [HideInInspector]
    public SteamVR_Controller.Device Controller;
#endif

    public void Awake()
    {
        handActions = GetComponentInChildren<HandActions>();

        trackedObj = GetComponent<SteamVR_TrackedObject>();

#if TEST
        Controller = SteamVR_Controller.Input((int)trackedObj.index);
#endif
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (collidingObject != null)
            {
                print("interact");
                Interact();
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (interactingObject != null)
            {
                print("deinteract");
                DeInteract();
            }
        }

        if (Controller == null)
        {
            return;
        }

        Vector2 triggerAxis = Controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger);
        handActions.timeline = triggerAxis.x;

        HandleButtonPresses();
    }

    private void HandleButtonPresses()
    {
        // If the trigger gets pressed down and there is a colliding object, interact with it.
        if (Controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger))
        {
            if (collidingObject != null)
            {
                Interact();
            }
        }

        // If the trigger gets pressed up and the player is interacting with an object, deinteract with it.
        if (Controller.GetPressUp(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger))
        {
            if (interactingObject != null)
            {
                DeInteract();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // If the player is already interacting with an object, ignore new collisions.
        if (interactingObject != null)
        {
            return;
        }

        // Try to find an interactable component on the colliding object.
        Interactable interactable = other.gameObject.GetComponent<Interactable>();

        // If theres already a colliding object, check if were colliding with another interactable and if collidingObject has a highlight component and if so, dehighlight it.
        if (collidingObject != null)
        {
            if (interactable != null)
            {
                Highlightable highlightable = collidingObject.GetComponent<Highlightable>();
                if (highlightable != null)
                {
                    highlightable.DeHighlight();
                }
            }
        }

        // If were currently not colliding with the object our other hand is colliding with, highlight our current object.
        if (otherHand != null)
        {
            if (otherHand.interactingObject == null || otherHand.interactingObject.gameObject != other.gameObject)
            {
                Highlightable highlightable = other.gameObject.GetComponent<Highlightable>();
                if (highlightable != null)
                {
                    if (Controller != null && !highlightable.isHighlighted)
                    {
                        Controller.TriggerHapticPulse((ushort)VRPlayerMovementManager.instance.controllerHapticPulse);
                    }

                    highlightable.Highlight();
                }
            }
        }

        // If the current colliding object is an interactable, set the collidingObject to that object and change the hand animation based on the type of interactable.
        if (interactable != null)
        {
            collidingObject = other.gameObject;
            handActions.press = (interactable is Clickable ? true : false);
        }
    }

    //public void OnTriggerEnter(Collider other)
    //{
    //    // If the player is already interacting with an object, ignore new collisions.
    //    if (interactingObject != null)
    //    {
    //        return;
    //    }

    //    // Try to find an interactable component on the colliding object.
    //    Interactable interactable = other.gameObject.GetComponent<Interactable>();

    //    // If theres already a colliding object, check if were colliding with another interactable and if collidingObject has a highlight component and if so, dehighlight it.
    //    if (collidingObject != null)
    //    {
    //        if (interactable != null)
    //        {
    //            Highlightable highlightable = collidingObject.GetComponent<Highlightable>();
    //            if (highlightable != null)
    //            {
    //                highlightable.DeHighlight();
    //            }
    //        }
    //    }

    //    // If were currently not colliding with the object our other hand is colliding with, highlight our current object.
    //    if (otherHand != null)
    //    {
    //        if (otherHand.interactingObject == null || otherHand.interactingObject.gameObject != other.gameObject)
    //        {
    //            Highlightable highlightable = other.gameObject.GetComponent<Highlightable>();
    //            if (highlightable != null)
    //            {
    //                highlightable.Highlight();

    //                if (Controller != null)
    //                {
    //                    Controller.TriggerHapticPulse((ushort)VRPlayerMovementManager.instance.controllerHapticPulse);
    //                }
    //            }
    //        }
    //    }

    //    // If the current colliding object is an interactable, set the collidingObject to that object and change the hand animation based on the type of interactable.
    //    if (interactable != null)
    //    {
    //        collidingObject = other.gameObject;
    //        handActions.press = (interactable is Clickable ? true : false);
    //    }
    //}

    public void OnTriggerExit(Collider other)
    {
        // If the exiting object is not our collidingObject, ignore this function.
        if (other.gameObject != collidingObject)
        {
            return;
        }

        // If the exiting object has a highlight component, dehighlight it.
        Highlightable highlightable = other.GetComponent<Highlightable>();
        if (highlightable != null)
        {
            highlightable.DeHighlight();
        }

        // If the exiting object is an interactable, reset our hand animation to the default grab animation.
        Interactable interactable = other.GetComponent<Interactable>();
        if (interactable != null)
        {
            handActions.press = false;
        }

        // Set collidingObject to null since it has exited our trigger.
        collidingObject = null;
    }

    private void Interact()
    {
        // Check if the object were supposed to interact with is indeed an interactable, if not then return.
        Interactable interactable = collidingObject.GetComponent<Interactable>();
        if (interactable == null)
        {
            return;
        }

        // If the other hand is interacting with the object this hand wants to interact with, deinteract it from the other hand.
        if (otherHand != null)
        {
            if (otherHand.interactingObject != null && otherHand.interactingObject.gameObject == collidingObject)
            {
                otherHand.DeInteract();
            }
        }

        // Check if the object we wanna interact with has a highlight component and deactivate it if it has one.
        Highlightable highlightable = collidingObject.GetComponent<Highlightable>();
        if (highlightable != null)
        {
            highlightable.DeHighlight();
        }

        // Interact with the object and set the interactingObject.
        interactable.Interact(this);
        interactingObject = interactable;
        //collidingObject = null;
    }

    public void DeInteract()
    {
        // Deinteract with the interactingObject and set it to null.
        interactingObject.DeInteract(this);
        interactingObject = null;
    }
}
