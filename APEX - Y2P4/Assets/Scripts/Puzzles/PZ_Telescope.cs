using UnityEngine;
using TMPro;

public class PZ_Telescope : MonoBehaviour 
{

    [Header("Telescope Cam")]
    [SerializeField]
    private Camera telescopeCam;
    [SerializeField]
    private float minFOV;
    [SerializeField]
    private float maxFOV;
    [SerializeField]
    private TextMeshProUGUI fovText;

    [Space(10)]

    [Header("Telescope Parts")]
    [SerializeField]
    private Transform telescopeX;
    [SerializeField]
    private Transform telescopeY;

    [Space(10)]

    [Header("Telescope Restrictions")]
    [SerializeField]
    private float minVertical;
    [SerializeField]
    private float maxVertical;

    [Header("Telescope Search Display")]
    [SerializeField]
    private float rayLength = 1000f;
    [SerializeField]
    private LayerMask rayLayerMask;
    [SerializeField]
    private TextMeshProUGUI spaceObjectNameText;
    [SerializeField]
    private TextMeshProUGUI spaceObjectMassText;
    [SerializeField]
    private TextMeshProUGUI spaceObjectDistanceText;
    [SerializeField]
    private GameObject searchingForObjectsOverlay;
    [SerializeField]
    private GameObject spaceObjectInfoOverlay;

    private Transform lastSpaceObjectHit;
    private enum TelescopeOptions { Horizontal, Vertical, FOV }
    private Transform fovReference;
    private Transform horizontalReference;
    private float horizontalReferenceZRot;
    private Transform verticalReference;
    private float verticalReferenceZRot;

    private void Awake()
    {
        telescopeCam.fieldOfView = ((minFOV + maxFOV) / 2) + 0.47219f;
        fovText.text = (telescopeCam.fieldOfView < 10) ? "fov 0" + telescopeCam.fieldOfView : "fov " + telescopeCam.fieldOfView;
    }

    private void Update()
    {
        HandleHorizontalMovement();
        HandleVerticalMovement();
        HandleFOV();
        HandleRaycast();
    }

    private void HandleHorizontalMovement()
    {
        if (horizontalReference != null)
        {
            float deltaHorizontal = horizontalReference.localEulerAngles.z - horizontalReferenceZRot;
            horizontalReferenceZRot = horizontalReference.localEulerAngles.z;

            float newEulerZ = telescopeX.localEulerAngles.z - deltaHorizontal;
            telescopeX.localEulerAngles = new Vector3(telescopeX.localEulerAngles.x, telescopeX.localEulerAngles.y, newEulerZ);
        }
    }

    private void HandleVerticalMovement()
    {
        if (verticalReference != null)
        {
            float deltaHorizontal = verticalReference.localEulerAngles.z - verticalReferenceZRot;
            verticalReferenceZRot = verticalReference.localEulerAngles.z;
            if (Mathf.Abs(deltaHorizontal) > 100)
            {
                return;
            }

            float newEulerZ = telescopeY.localEulerAngles.z - deltaHorizontal;
            newEulerZ = GetPositiveClampedAngle(newEulerZ);
            telescopeY.localEulerAngles = new Vector3(telescopeY.localEulerAngles.x, telescopeY.localEulerAngles.y, newEulerZ);
        }
    }

    private void HandleFOV()
    {
        if (fovReference != null)
        {
            telescopeCam.fieldOfView = Remap(fovReference.localPosition.z, -0.1f, 0.1f, minFOV, maxFOV);
            fovText.text = (telescopeCam.fieldOfView < 10) ? "fov 0" + telescopeCam.fieldOfView : "fov " + telescopeCam.fieldOfView;
        }
    }

    private void HandleRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(telescopeCam.transform.position, telescopeCam.transform.forward, out hit, rayLength, rayLayerMask))
        {
            if (hit.transform == lastSpaceObjectHit)
            {
                searchingForObjectsOverlay.SetActive(false);
                spaceObjectInfoOverlay.SetActive(true);
                return;
            }

            SpaceObject so = hit.transform.GetComponent<SpaceObject>();
            if (so != null)
            {
                searchingForObjectsOverlay.SetActive(false);

                spaceObjectNameText.text = so.objectName;
                spaceObjectMassText.text = "Mass: " + so.objectMass.ToString() + " x 10^24kg";
                spaceObjectDistanceText.text = "Dist. " + so.objectDistance.ToString() + "M km";

                spaceObjectInfoOverlay.SetActive(true);

                lastSpaceObjectHit = hit.transform;
            }
            else
            {
                spaceObjectInfoOverlay.SetActive(false);
                searchingForObjectsOverlay.SetActive(true);
            }
        }
        else
        {
            spaceObjectInfoOverlay.SetActive(false);
            searchingForObjectsOverlay.SetActive(true);
        }
    }

    public void ChangeHorizontalRot(Transform t)
    {
        horizontalReference = t;
        horizontalReferenceZRot = t.localEulerAngles.z;
    }

    public void StopChangingHorizontalRot()
    {
        horizontalReference = null;
    }

    public void ChangeVerticalRot(Transform t)
    {
        verticalReference = t;
        verticalReferenceZRot = t.localEulerAngles.z;
    }

    public void StopChangingVerticalRot()
    {
        verticalReference = null;
    }

    public void ChangeFOV(Transform t)
    {
        fovReference = t;
    }

    public void StopChangingFOV()
    {
        fovReference = null;
    }

    private float GetPositiveClampedAngle(float angle)
    {
        if (angle < 90 || angle > 270)
        {
            angle = (angle > 180) ? angle - 360 : angle;
            maxVertical = (maxVertical > 180) ? maxVertical - 360 : maxVertical;
            minVertical = (minVertical > 180) ? minVertical - 360 : minVertical;
        }

        if (minVertical != 0 && maxVertical != 0)
        {
            angle = Mathf.Clamp(angle, minVertical, maxVertical);
        }

        angle = (angle < 0) ? angle + 360 : angle;

        return angle;
    }

    private float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
