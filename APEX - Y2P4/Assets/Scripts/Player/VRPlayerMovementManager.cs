using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class VRPlayerMovementManager : MonoBehaviour
{

    public static VRPlayerMovementManager instance;

    public enum MovementType
    {
        Teleportation,
        TouchpadWalking
    }
    public MovementType movementType;

    [HideInInspector]
    public Transform cameraRigTransform;
    [HideInInspector]
    public Transform headTransform;

    public float controllerHapticPulse = 500f;

    [Space(10)]

    public VRInteractor leftHand;
    public VRInteractor rightHand;
    [SerializeField]
    private Transform eyeTransform;

    private Vector3 handBasePos;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        cameraRigTransform = transform;
        headTransform = Camera.main.transform;

        handBasePos = leftHand.transform.GetChild(0).transform.localPosition;

        Valve.VR.OpenVR.System.ResetSeatedZeroPose();
        Valve.VR.OpenVR.Compositor.SetTrackingSpace(Valve.VR.ETrackingUniverseOrigin.TrackingUniverseSeated);
    }

    private void Start()
    {
        transform.position -= new Vector3(eyeTransform.localPosition.x, 0, eyeTransform.localPosition.z);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            SwapHands();
        }
    }

    public void SwitchMovementType()
    {
        if (movementType == MovementType.TouchpadWalking)
        {
            movementType = MovementType.Teleportation;
        }
        else
        {
            movementType = MovementType.TouchpadWalking;
        }
    }

    public void SwapHands()
    {
        Transform leftHandModel = leftHand.transform.GetChild(0);

        rightHand.transform.GetChild(0).SetParent(leftHand.transform);
        leftHandModel.SetParent(rightHand.transform);

        leftHand.transform.GetChild(0).transform.localPosition = handBasePos;
        leftHand.transform.GetChild(0).transform.localRotation = Quaternion.Euler(Vector3.zero);
        rightHand.transform.GetChild(0).transform.localPosition = handBasePos;
        rightHand.transform.GetChild(0).transform.localRotation = Quaternion.Euler(Vector3.zero);

        leftHand.Awake();
        rightHand.Awake();
    }
}
