using UnityEngine;

public class PZ_Telescope : MonoBehaviour 
{

    [SerializeField]
    private Transform telescopeX;
    [SerializeField]
    private Transform telescopeY;

    private Transform horizontalReference;
    private float horizontalReferenceZRot;
    private Transform verticalReference;
    private float verticalReferenceZRot;

    private void Update()
    {
        if (horizontalReference != null)
        {
            float deltaHorizontal = horizontalReference.localEulerAngles.z - horizontalReferenceZRot;
            horizontalReferenceZRot = horizontalReference.localEulerAngles.z;

            float newEulerZ = telescopeX.localEulerAngles.z - deltaHorizontal;
            telescopeX.localEulerAngles = new Vector3(telescopeX.localEulerAngles.x, telescopeX.localEulerAngles.y, newEulerZ);
        }

        if (verticalReference != null)
        {
            float deltaHorizontal = verticalReference.localEulerAngles.z - verticalReferenceZRot;
            verticalReferenceZRot = verticalReference.localEulerAngles.z;

            float newEulerZ = telescopeY.localEulerAngles.z - deltaHorizontal;
            telescopeY.localEulerAngles = new Vector3(telescopeY.localEulerAngles.x, telescopeY.localEulerAngles.y, newEulerZ);
        }
    }

    public void ChangeHorizontalRot(Transform t)
    {
        horizontalReference = t;
        horizontalReferenceZRot = t.localEulerAngles.z;
    }

    public void StopChaingHorizontalRot()
    {
        horizontalReference = null;
    }

    public void ChangeVerticalRot(Transform t)
    {
        verticalReference = t;
        verticalReferenceZRot = t.localEulerAngles.z;
    }

    public void StopChangingVerticalRot()
    {
        verticalReference = null;
    }
}
