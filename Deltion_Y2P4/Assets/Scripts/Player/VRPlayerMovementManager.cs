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
    }
}
