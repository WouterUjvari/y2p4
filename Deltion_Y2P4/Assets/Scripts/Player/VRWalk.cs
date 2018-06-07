using UnityEngine;

public class VRWalk : MonoBehaviour
{

    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device controller;

    [SerializeField]
    private float moveSpeed;

    private void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        controller = SteamVR_Controller.Input((int)trackedObj.index);
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
        if (controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
            Vector2 touchpad = controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad);

            float speed = Time.deltaTime * moveSpeed;

			Vector3 nextPos = (VRPlayerMovementManager.instance.headTransform.transform.right * (touchpad.x * speed) + VRPlayerMovementManager.instance.headTransform.transform.forward * (touchpad.y * speed)) * Time.deltaTime;
			nextPos.y = 0;
			VRPlayerMovementManager.instance.cameraRigTransform.transform.position += nextPos;
		}
    }
}
