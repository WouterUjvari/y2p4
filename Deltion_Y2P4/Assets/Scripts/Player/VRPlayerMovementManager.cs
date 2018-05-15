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

	public Transform headTransform;

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
    }
}
