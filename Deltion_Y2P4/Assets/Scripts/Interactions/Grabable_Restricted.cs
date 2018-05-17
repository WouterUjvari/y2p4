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

    private void Update()
    {
        if (!restrict)
        {
            return;
        }

        switch (axisToRestrict)
        {
            case AxisToRestrict.X:

                transform.position = new Vector3(restrictedAxis.x, transform.position.y, transform.position.z);
                break;
            case AxisToRestrict.XY:

                transform.position = new Vector3(restrictedAxis.x, restrictedAxis.y, transform.position.z);
                break;
            case AxisToRestrict.XZ:

                transform.position = new Vector3(restrictedAxis.x, transform.position.y, restrictedAxis.z);
                break;
            case AxisToRestrict.Y:

                transform.position = new Vector3(transform.position.x, restrictedAxis.y, transform.position.z);
                break;
            case AxisToRestrict.YZ:

                transform.position = new Vector3(transform.position.x, restrictedAxis.y, restrictedAxis.z);
                break;
            case AxisToRestrict.Z:

                transform.position = new Vector3(transform.position.x, transform.position.y, restrictedAxis.z);
                break;
        }

        transform.rotation = restrictedRot;
    }

    public override void Interact(VRInteractor hand)
    {
        base.Interact(hand);

        restrict = true;
        LockRestrictedAxis();
    }

    public override void DeInteract(VRInteractor hand)
    {
        base.DeInteract(hand);

        restrict = false;
        LockRestrictedAxis();
    }

    private void LockRestrictedAxis()
    {
        restrictedAxis = transform.position;
        restrictedRot = transform.rotation;
    }
}
