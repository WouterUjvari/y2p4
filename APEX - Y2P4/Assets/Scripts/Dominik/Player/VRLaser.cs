using UnityEngine;

public class VRLaser : MonoBehaviour
{

    #region Private References
    private SteamVR_TrackedObject trackedObj;

    private GameObject laser;
    private Transform laserTransform;

    private Vector3 hitPoint;

    private GameObject teleportReticle;
    private Transform teleportReticleTransform;

    private bool canTeleport;
    #endregion

    [SerializeField]
    private GameObject laserPrefab;
    [SerializeField]
    private Material laserMat;

    [SerializeField]
    private Transform headTransform;

    [Header("Teleport Reticle")]
    [SerializeField]
    private GameObject teleportReticlePrefab;
    [SerializeField]
    private Vector3 teleportReticleOffset;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    private void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    private void Start()
    {
        laser = Instantiate(laserPrefab);
        laserTransform = laser.transform;
        laser.SetActive(false);

        teleportReticle = Instantiate(teleportReticlePrefab);
        teleportReticleTransform = teleportReticle.transform;
        teleportReticle.SetActive(false);
    }

    private void Update()
    {
        if (VRPlayerMovementManager.instance.movementType == VRPlayerMovementManager.MovementType.Teleportation && VRPlayerMovementManager.canMove)
        {
            HandleLaser();
        }
    }

    private void HandleLaser()
    {
        if (Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
            RaycastHit hit;
            if (Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, 50))
            {
                hitPoint = hit.point;
                ShowLaser(hit);

                if (hit.transform.tag == "Ground")
                {
                    laserMat.color = Color.green;

                    teleportReticle.SetActive(true);
                    teleportReticleTransform.position = hitPoint + teleportReticleOffset;

                    canTeleport = true;
                }
                else
                {
                    laserMat.color = Color.red;

                    teleportReticle.SetActive(false);

                    canTeleport = false;
                }
            }
        }
        else
        {
            laser.SetActive(false);
            teleportReticle.SetActive(false);
        }

        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            if (canTeleport)
            {
                Teleport();
                return;
            }
        }
    }

    private void ShowLaser(RaycastHit hit)
    {
        laser.SetActive(true);

        laserTransform.position = Vector3.Lerp(trackedObj.transform.position, hitPoint, .5f);
        laserTransform.LookAt(hitPoint);
        laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y, hit.distance);
    }

    private void Teleport()
    {
        canTeleport = false;
        teleportReticle.SetActive(false);

        Vector3 difference = VRPlayerMovementManager.instance.cameraRigTransform.position - headTransform.position;
        difference.y = 0;

        VRPlayerMovementManager.instance.cameraRigTransform.position = hitPoint + difference;
    }
}
