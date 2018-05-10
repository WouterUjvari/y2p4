using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRLaser : MonoBehaviour
{

    #region Private References
    private SteamVR_TrackedObject trackedObj;

    private GameObject laser;
    private Transform laserTransform;
    private Vector3 hitPoint;
    private Transform raycastHit;

    private GameObject teleportReticle;
    private Transform teleportReticleTransform;

    private GameObject interactReticle;
    private Transform interactReticleTransform;

    private bool canTeleport;
    //private bool canInteract;
    #endregion

    public GameObject laserPrefab;
    public Material laserMat;

    public Transform cameraRigTransform;

    public Transform headTransform;

    [Header("Teleport Reticle")]
    public GameObject teleportReticlePrefab;
    public Vector3 teleportReticleOffset;

    //[Header("Interact Reticle")]
    //public GameObject interactReticlePrefab;
    //public Vector3 interactReticleOffset;

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

        teleportReticle = Instantiate(teleportReticlePrefab);
        teleportReticleTransform = teleportReticle.transform;

        //interactReticle = Instantiate(interactReticlePrefab);
        //interactReticleTransform = interactReticle.transform;
    }

    private void Update()
    {
        if (Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
            RaycastHit hit;
            if (Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, 50))
            {
                hitPoint = hit.point;
                raycastHit = hit.transform;
                ShowLaser(hit);

                if (hit.transform.tag == "Ground")
                {
                    laserMat.color = Color.green;

                    teleportReticle.SetActive(true);
                    //interactReticle.SetActive(false);
                    teleportReticleTransform.position = hitPoint + teleportReticleOffset;

                    canTeleport = true;
                }
                //else if (hit.transform.tag == "Interactable")
                //{
                //    laserMat.color = Color.green;

                //    teleportReticle.SetActive(false);
                //    interactReticle.SetActive(true);
                //    interactReticleTransform.position = hitPoint + interactReticleOffset;

                //    canTeleport = false;
                //    canInteract = true;
                //}
                else
                {
                    laserMat.color = Color.red;

                    teleportReticle.SetActive(false);
                    interactReticle.SetActive(false);

                    canTeleport = false;
                    //canInteract = false;
                }
            }
        }
        else
        {
            laser.SetActive(false);
            teleportReticle.SetActive(false);
            //interactReticle.SetActive(false);
        }

        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            if (canTeleport)
            {
                Teleport();
                return;
            }

            //if (canInteract)
            //{
            //    raycastHit.GetComponentInParent<Animator>().SetTrigger("Trigger");

            //    canInteract = false;
            //    interactReticle.SetActive(false);
            //    return;
            //}
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

        Vector3 difference = cameraRigTransform.position - headTransform.position;
        difference.y = 0;

        cameraRigTransform.position = hitPoint + difference;
    }
}
