using UnityEngine;

public class Grabable_Restricted : Grabable 
{

    private bool restrict;

    public enum AxisToRestrict
    {
        X,
        XY,
        XZ,
        Y,
        YZ,
        Z
    }
    public AxisToRestrict axisToRestrict;

    private Vector3 restrictedAxis;
    private Quaternion restrictedRot;

    private Vector3 defaultPos;

    [SerializeField]
    private float maxMoveAmount = 5f;

    private VRInteractor interactingHand;
    private Vector3 interactingHandPos;

    public override void Awake()
    {
        base.Awake();

        defaultPos = transform.position;
    }

    private void Update()
    {
        if (!restrict)
        {
            return;
        }

		Vector3 deltaPos = interactingHand.transform.position - interactingHandPos;
        interactingHandPos = interactingHand.transform.position;

        switch (axisToRestrict)
        {
            case AxisToRestrict.X:

                //transform.position += new Vector3(0, deltaPos.y, deltaPos.z);
                transform.position = new Vector3(transform.position.x, GetClampedAxis(transform.position.y + deltaPos.y, "y"), GetClampedAxis(transform.position.z + deltaPos.z, "z"));
                break;
            case AxisToRestrict.XY:

                transform.position = new Vector3(transform.position.x, transform.position.y, GetClampedAxis(transform.position.z + deltaPos.z, "z"));
                break;
            case AxisToRestrict.XZ:

                transform.position = new Vector3(transform.position.x, GetClampedAxis(transform.position.y + deltaPos.y, "y"), transform.position.z);
                break;
            case AxisToRestrict.Y:

                transform.position = new Vector3(GetClampedAxis(transform.position.x + deltaPos.x, "x"), transform.position.y, GetClampedAxis(transform.position.z + deltaPos.z, "z"));
                break;
            case AxisToRestrict.YZ:

                transform.position = new Vector3(GetClampedAxis(transform.position.x + deltaPos.x, "x"), transform.position.y, transform.position.z);
                break;
            case AxisToRestrict.Z:

                transform.position = new Vector3(GetClampedAxis(transform.position.x + deltaPos.x, "x"), GetClampedAxis(transform.position.y + deltaPos.y, "y"), transform.position.z);
                break;
        }

        transform.rotation = restrictedRot;
    }
		
    public override void Interact(VRInteractor hand)
    {
        Grab(hand);
    }

    public override void DeInteract(VRInteractor hand)
    {
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

                Mathf.Clamp(axisVar, defaultPos.x - maxMoveAmount, defaultPos.x + maxMoveAmount);
                break;
            case "y":

                Mathf.Clamp(axisVar, defaultPos.y - maxMoveAmount, defaultPos.y + maxMoveAmount);
                break;
            case "z":

                Mathf.Clamp(axisVar, defaultPos.z - maxMoveAmount, defaultPos.z + maxMoveAmount);
                break;
        }

        return axisVar;
    }
}
