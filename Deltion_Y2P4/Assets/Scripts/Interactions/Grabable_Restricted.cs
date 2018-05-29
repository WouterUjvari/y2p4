using UnityEngine;

public class Grabable_Restricted : Grabable 
{

    private bool restrict;

    private enum AxisToRestrict
    {
        X,
        XY,
        XZ,
        Y,
        YZ,
        Z
    }
    [SerializeField]
    private AxisToRestrict axisToRestrict;

    private Vector3 restrictedAxis;
    private Quaternion restrictedRot;

    private Vector3 defaultPos;

    [SerializeField]
    private Transform interactPoint;

    [Space(10)]

    [SerializeField]
    private float moveMultiplier = 200f;
    [SerializeField]
    private float maxMoveAmount = 5f;
    [SerializeField]
    private float interactBreakDistance = 0.5f;

    private VRInteractor interactingHand;
    private Vector3 interactingHandPos;

    public override void Awake()
    {
        base.Awake();

        defaultPos = transform.localPosition;

        if (interactPoint == null)
        {
            interactPoint = transform;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Interact(testHand);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            DeInteract(testHand);
        }

        if (!restrict)
        {
            return;
        }

        if (Vector3.Distance(interactingHand.transform.position, interactPoint.position) > interactBreakDistance)
        {
            DeInteract(interactingHand);
            return;
        }

        Vector3 deltaPos = interactingHand.transform.position - interactingHandPos;
        interactingHandPos = interactingHand.transform.position;

        float deltaDistance = (deltaPos.x + deltaPos.y + deltaPos.z) / 3;
        float deltaChange = (deltaDistance * Time.deltaTime) * moveMultiplier;

        switch (axisToRestrict)
        {
            case AxisToRestrict.X:

                transform.localPosition += new Vector3(0, deltaChange, deltaChange);
                //transform.localPosition = new Vector3(transform.localPosition.x, GetClampedAxis(transform.localPosition.y + deltaChange, "y"), GetClampedAxis(transform.localPosition.z + deltaChange, "z"));
                break;
            case AxisToRestrict.XY:

                transform.localPosition += new Vector3(0, 0, deltaChange);
                //transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, GetClampedAxis(transform.localPosition.z + deltaChange, "z"));
                break;
            case AxisToRestrict.XZ:

                transform.localPosition += new Vector3(0, deltaChange, 0);
                //transform.localPosition = new Vector3(transform.localPosition.x, GetClampedAxis(transform.localPosition.y + deltaChange, "y"), transform.localPosition.z);
                break;
            case AxisToRestrict.Y:

                transform.localPosition += new Vector3(deltaChange, 0, deltaChange);
                //transform.localPosition = new Vector3(GetClampedAxis(transform.localPosition.x + deltaChange, "x"), transform.localPosition.y, GetClampedAxis(transform.localPosition.z + deltaChange, "z"));
                break;
            case AxisToRestrict.YZ:

                transform.localPosition += new Vector3(deltaChange, 0, 0);
                //transform.localPosition = new Vector3(GetClampedAxis(transform.localPosition.x + deltaChange, "x"), transform.localPosition.y, transform.localPosition.z);
                break;
            case AxisToRestrict.Z:

                transform.localPosition += new Vector3(deltaChange, deltaChange, 0);
                //transform.localPosition = new Vector3(GetClampedAxis(transform.localPosition.x + deltaChange, "x"), GetClampedAxis(transform.localPosition.y + deltaChange, "y"), transform.localPosition.z);
                break;
        }

        transform.rotation = restrictedRot;
    }
		
    public override void Interact(VRInteractor hand)
    {
        base.Interact(hand);

        Grab(hand);
    }

    public override void DeInteract(VRInteractor hand)
    {
        base.DeInteract(hand);

        Release(hand);
    }

    public override void Grab(VRInteractor hand)
    {
        interactingHand = hand;
        interactingHandPos = hand.transform.position;

        restrict = true;
        LockRestrictedAxis();
    }

    public override void Release(VRInteractor hand)
    {
        restrict = false;
        interactingHand = null;
    }

    private void LockRestrictedAxis()
    {
        restrictedAxis = transform.position;
        restrictedRot = transform.rotation;
    }

    private void ResetRigidbodyConstraints()
    {
        rb.constraints = RigidbodyConstraints.None;
    }

    private float GetClampedAxis(float axisVar, string axisName)
    {
        float f = axisVar;

        switch (axisName)
        {
            case "x":

                f = Mathf.Clamp(f, defaultPos.x - maxMoveAmount, defaultPos.x + maxMoveAmount);
                break;
            case "y":

                f = Mathf.Clamp(f, defaultPos.y - maxMoveAmount, defaultPos.y + maxMoveAmount);
                break;
            case "z":

                f = Mathf.Clamp(f, defaultPos.z - maxMoveAmount, defaultPos.z + maxMoveAmount);
                break;
        }

        return f;
    }
}
