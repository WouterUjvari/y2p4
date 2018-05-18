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

    private Rigidbody rb;

    private Vector3 defaultPos;

    [SerializeField]
    private float maxMoveAmount;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        defaultPos = transform.position;
    }

    private void Update()
    {
        if (!restrict)
        {
            return;
        }

        switch (axisToRestrict)
        {
            case AxisToRestrict.X:

                transform.position = new Vector3(restrictedAxis.x, GetClampedAxis(transform.position.y), GetClampedAxis(transform.position.z));
                rb.constraints = RigidbodyConstraints.FreezePositionX;
                break;
            case AxisToRestrict.XY:

                transform.position = new Vector3(restrictedAxis.x, restrictedAxis.y, GetClampedAxis(transform.position.z));
                rb.constraints = RigidbodyConstraints.FreezePositionX;
                rb.constraints = RigidbodyConstraints.FreezePositionY;
                break;
            case AxisToRestrict.XZ:

                transform.position = new Vector3(restrictedAxis.x, GetClampedAxis(transform.position.y), restrictedAxis.z);
                rb.constraints = RigidbodyConstraints.FreezePositionX;
                rb.constraints = RigidbodyConstraints.FreezePositionZ;
                break;
            case AxisToRestrict.Y:

                transform.position = new Vector3(GetClampedAxis(transform.position.x), restrictedAxis.y, GetClampedAxis(transform.position.z));
                rb.constraints = RigidbodyConstraints.FreezePositionY;
                break;
            case AxisToRestrict.YZ:

                transform.position = new Vector3(GetClampedAxis(transform.position.x), restrictedAxis.y, restrictedAxis.z);
                rb.constraints = RigidbodyConstraints.FreezePositionY;
                rb.constraints = RigidbodyConstraints.FreezePositionZ;
                break;
            case AxisToRestrict.Z:

                transform.position = new Vector3(GetClampedAxis(transform.position.x), GetClampedAxis(transform.position.y), restrictedAxis.z);
                rb.constraints = RigidbodyConstraints.FreezePositionZ;
                break;
        }

        transform.rotation = restrictedRot;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    public override void Interact(VRInteractor hand)
    {
        base.Interact(hand);

        restrict = true;
        LockRestrictedAxis();
        ResetRigidbodyConstraints();
    }

    public override void DeInteract(VRInteractor hand)
    {
        base.DeInteract(hand);

        restrict = false;
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

    private float GetClampedAxis(float axis)
    {
        Mathf.Clamp(axis, axis - maxMoveAmount, axis + maxMoveAmount);
        return axis;
    }
}
