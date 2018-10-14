using UnityEngine;

public class VRWalk : MonoBehaviour
{

    [SerializeField]
    private LayerMask moveDetectionLayerMask;

    private SteamVR_TrackedObject trackedObj;
    private RaycastHit hit;

    private SteamVR_Controller.Device controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    [SerializeField]
    private float moveSpeed;

    private void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    private void Update()
    {
        if (VRPlayerMovementManager.instance.movementType == VRPlayerMovementManager.MovementType.TouchpadWalking && VRPlayerMovementManager.canMove)
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
            Vector3 newPos = nextPos + VRPlayerMovementManager.instance.headTransform.transform.position;

            Debug.DrawRay(newPos, Vector3.down, Color.red);
            if (!Physics.Raycast(newPos,Vector3.down,out hit, 25, moveDetectionLayerMask))
            {
                VRPlayerMovementManager.instance.cameraRigTransform.transform.position += nextPos;

                // Lerp.
                //Vector3 newValidPos = VRPlayerMovementManager.instance.cameraRigTransform.position + nextPos;
                //VRPlayerMovementManager.instance.cameraRigTransform.position = Vector3.Lerp(VRPlayerMovementManager.instance.cameraRigTransform.position, newValidPos, Time.deltaTime);
            }

            #region OldTagMethod
            //if (Physics.Raycast(newPos, Vector3.down, out hit))
            //{
            //    if (hit.transform.tag != "CantMove")
            //    {
            //        VRPlayerMovementManager.instance.cameraRigTransform.transform.position += nextPos;
            //    }
            //}
            #endregion
        }
    }
}
