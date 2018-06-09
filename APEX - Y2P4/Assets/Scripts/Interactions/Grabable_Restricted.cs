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
    [SerializeField]
    private Transform interactPoint;

    [Space(10)]

    [SerializeField]
    private float moveMultiplier = 200f;
    [SerializeField]
    private float maxMoveAmount = 1f;
    [SerializeField]
    private float interactBreakDistance = 0.5f;
    [SerializeField]
    private Vector3 startingLocalPosition;
    [SerializeField]
    private bool ignoreHandYMovment;
    [SerializeField]
    private bool invert;

    private Vector3 restrictedAxis;
    private Quaternion restrictedRot;
    private Vector3 defaultPos;
    private Vector3 interactingHandPos;

    public override void Awake()
    {
        base.Awake();

        defaultPos = transform.localPosition;

        if (interactPoint == null)
        {
            interactPoint = transform;
        }

        transform.localPosition = startingLocalPosition;
    }

    private void Update()
    {
        if (testHand != null)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                Interact(testHand);
            }
            if (Input.GetKeyDown(KeyCode.V))
            {
                DeInteract(testHand);
            }
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

        float deltaDistance = 0;
        if (ignoreHandYMovment)
        {
            deltaDistance = (deltaPos.x + deltaPos.z) / 2;
        }
        else
        {
            deltaDistance = (deltaPos.x + deltaPos.y + deltaPos.z) / 3;
        }
        float deltaChange = (deltaDistance * Time.deltaTime) * moveMultiplier;
        deltaChange = invert ? -deltaChange : deltaChange;

        Vector3 currentPos = transform.localPosition;
        Vector3 targetPos = currentPos;

        switch (axisToRestrict)
        {
            case AxisToRestrict.X:

                targetPos = (currentPos += new Vector3(0, deltaChange, deltaChange));
                targetPos.y = GetClampedAxis(targetPos.y, "y");
                targetPos.z = GetClampedAxis(targetPos.z, "z");
                break;
            case AxisToRestrict.XY:

                targetPos = (currentPos += new Vector3(0, 0, deltaChange));
                targetPos.z = GetClampedAxis(targetPos.z, "z");
                break;
            case AxisToRestrict.XZ:

                targetPos = (currentPos += new Vector3(0, deltaChange, 0));
                targetPos.y = GetClampedAxis(targetPos.y, "y");
                break;
            case AxisToRestrict.Y:

                targetPos = (currentPos += new Vector3(deltaChange, 0, deltaChange));
                targetPos.x = GetClampedAxis(targetPos.x, "x");
                targetPos.z = GetClampedAxis(targetPos.z, "z");
                break;
            case AxisToRestrict.YZ:

                targetPos = (currentPos += new Vector3(deltaChange, 0, 0));
                targetPos.x = GetClampedAxis(targetPos.x, "x");
                break;
            case AxisToRestrict.Z:

                targetPos = (currentPos += new Vector3(deltaChange, deltaChange, 0));
                targetPos.x = GetClampedAxis(targetPos.x, "x");
                targetPos.y = GetClampedAxis(targetPos.y, "y");
                break;
        }

        transform.localPosition = targetPos;
        transform.rotation = restrictedRot;
    }

    public override void Interact(VRInteractor hand)
    {
        if (locked)
        {
            return;
        }

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
        switch (axisName)
        {
            case "x":

                axisVar = Mathf.Clamp(axisVar, defaultPos.x - maxMoveAmount, defaultPos.x + maxMoveAmount);
                break;
            case "y":

                axisVar = Mathf.Clamp(axisVar, defaultPos.y - maxMoveAmount, defaultPos.y + maxMoveAmount);
                break;
            case "z":

                axisVar = Mathf.Clamp(axisVar, defaultPos.z - maxMoveAmount, defaultPos.z + maxMoveAmount);
                break;
        }

        return axisVar;
    }
}
