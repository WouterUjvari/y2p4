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
        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            Vector2 touchpad = Controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad);

            float speed = Time.deltaTime * moveSpeed;
            VRPlayerMovementManager.instance.cameraRigTransform.transform.position += new Vector3(touchpad.x * speed, VRPlayerMovementManager.instance.cameraRigTransform.position.y, touchpad.y * speed);

            //if (touchpad.y > 0.2f || touchpad.y < -0.2f)
            //{
            //    VRPlayerMovementManager.instance.cameraRigTransform.transform.position -= VRPlayerMovementManager.instance.cameraRigTransform.transform.forward * Time.deltaTime * (touchpad.y * 5f);
            //}
        }
    }
}
