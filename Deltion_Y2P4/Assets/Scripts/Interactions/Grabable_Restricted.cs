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
    private Vector3 currentPos;

    [SerializeField]
    private float maxMoveAmount;

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

        currentPos = transform.position;
        transform.rotation = restrictedRot;
    }

    private void LateUpdate()
    {
        if (!restrict)
        {
            return;
        }

        Vector3 deltaPos = (transform.position - currentPos);
        print(deltaPos);
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
        restrict = true;
        LockRestrictedAxis();
    }

    public override void Release(VRInteractor hand)
    {
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
