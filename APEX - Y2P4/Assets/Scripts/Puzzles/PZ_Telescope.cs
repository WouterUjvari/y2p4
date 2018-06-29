using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class PZ_Telescope : Puzzle
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

    [Header("Other")]
    [SerializeField]
    private Animator puzzleAnim;
    [SerializeField]
    private List<Interactable> planets = new List<Interactable>();
    [SerializeField]
    private ObjectSnapper objSnapper;
    [SerializeField]
    private List<PuzzlePiece> puzzle = new List<PuzzlePiece>();

    private Transform lastSpaceObjectHit;
    private enum TelescopeOptions { Horizontal, Vertical, FOV }
    private Transform fovReference;
    private Transform horizontalReference;
    private float horizontalReferenceZRot;
    private Transform verticalReference;
    private float verticalReferenceZRot;

    [System.Serializable]
    private struct PuzzlePiece
    {
        public ObjectSnapSpot snapSpot;
        public Transform planet;
    }

    private void Awake()
    {
        telescopeCam.fieldOfView = ((minFOV + maxFOV) / 2) + 0.47219f;
        fovText.text = (telescopeCam.fieldOfView < 10) ? "fov 0" + telescopeCam.fieldOfView : "fov " + telescopeCam.fieldOfView;

        ShufflePuzzle();
        if (HasCompletedPuzzle())
        {
            ShufflePuzzle();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            StartPuzzle();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            CompletePuzzle();
        }

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
            telescopeCam.fieldOfView = Mathf.Lerp(telescopeCam.fieldOfView, fovReference.localPosition.z.Remap(-0.1f, 0.1f, minFOV, maxFOV), Time.deltaTime * 10);
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

    public override void StartPuzzle()
    {
        FlowManager.instance.NextShipAIVoice(0);
        FlowManager.instance.NextAnouncerVoice(4);
        puzzleAnim.SetTrigger("Open");

        for (int i = 0; i < planets.Count; i++)
        {
            planets[i].Lock(false);
        }
    }

    public void CheckPuzzleProgress()
    {
        if (HasCompletedPuzzle())
        {
            CompletePuzzle();
        }
    }

    private bool HasCompletedPuzzle()
    {
        bool completed = true;

        for (int i = 0; i < puzzle.Count; i++)
        {
            if (puzzle[i].snapSpot.snappedObject != puzzle[i].planet)
            {
                completed = false;
            }
        }

        return completed;
    }

    private void ShufflePuzzle()
    {
        planets.Shuffle();
        for (int i = 0; i < planets.Count; i++)
        {
            planets[i].Lock(true);
            objSnapper.snapSpots[i].SnapObject(planets[i].transform);
        }
    }

    public override void CompletePuzzle()
    {
        FlowManager.instance.NextShipAIVoice(0);
        FlowManager.instance.NextAnouncerVoice(4);
        ExtraDroneFunctionality.instance.itemIndex = 0;
        ExtraDroneFunctionality.instance.triggerName = "GiveItem";
        Invoke("ExtraDroneFunctionality.instance.TriggerAnimation()",4);
        FlowManager.instance.NextPuzzle(12);

        for (int i = 0; i < planets.Count; i++)
        {
            planets[i].Lock(true);

            RotateAround[] rotateArounds = planets[i].GetComponentsInParent<RotateAround>();
            for (int ii = 0; ii < rotateArounds.Length; ii++)
            {
                if (rotateArounds[ii].transform != planets[i].transform)
                {
                    rotateArounds[ii].LockRotation();
                }
            }
        }
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
}
