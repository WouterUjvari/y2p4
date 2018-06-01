using System.Collections;
using System.Collections.Generic;
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

    [SerializeField]
    private Transform leftHand;
    [SerializeField]
    private Transform rightHand;

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

        handBasePos = leftHand.GetChild(0).transform.localPosition;
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
        Transform leftHandModel = leftHand.GetChild(0);

        rightHand.GetChild(0).SetParent(leftHand);
        leftHandModel.SetParent(rightHand);

        leftHand.GetChild(0).transform.localPosition = handBasePos;
        leftHand.GetChild(0).transform.localRotation = Quaternion.Euler(Vector3.zero);
        rightHand.GetChild(0).transform.localPosition = handBasePos;
        rightHand.GetChild(0).transform.localRotation = Quaternion.Euler(Vector3.zero);
    }
}
