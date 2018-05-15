using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRWalk : MonoBehaviour
{

    private SteamVR_TrackedObject trackedObj;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    public float moveSpeed;

    private void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    private void Update()
    {
        if (VRPlayerMovementManager.instance.movementType == VRPlayerMovementManager.MovementType.TouchpadWalking)
        {
            HandleMovement();
        }
    }

    private void HandleMovement()
    {
        if (Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
            Vector2 touchpad = Controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad);

            float speed = Time.deltaTime * moveSpeed;

			Vector3 nextPos = (VRPlayerMovementManager.instance.headTransform.transform.right * (touchpad.x * speed) + VRPlayerMovementManager.instance.headTransform.transform.forward * (touchpad.y * speed)) * Time.deltaTime;
			nextPos.y = VRPlayerMovementManager.instance.cameraRigTransform.transform.position.y;
			VRPlayerMovementManager.instance.cameraRigTransform.transform.position += nextPos;
		}
    }
}
