using System.Collections.Generic;
using System.Linq;
using UnityEditor;
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

    [Space(10)]

    public VRInteractor leftHand;
    public VRInteractor rightHand;
    [SerializeField]
    private Transform eyeTransform;

    [Header("Editor Testing")]
    [SerializeField]
    private bool editorTesting;

    private Vector3 handBasePos;

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

#if UNITY_EDITOR
        SetupEditorSymbols();
#endif

        cameraRigTransform = transform;
        headTransform = Camera.main.transform;

        handBasePos = leftHand.transform.GetChild(0).transform.localPosition;

        Valve.VR.OpenVR.System.ResetSeatedZeroPose();
        Valve.VR.OpenVR.Compositor.SetTrackingSpace(Valve.VR.ETrackingUniverseOrigin.TrackingUniverseSeated);
    }

    private void Start()
    {
        transform.position -= new Vector3(eyeTransform.localPosition.x, 0, eyeTransform.localPosition.z);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            SwapHands();
        }
    }

    public void SwitchMovementType()
    {
        if (movementType == MovementType.TouchpadWalking)
        {
            movementType = MovementType.Teleportation;
        }
        else
        {
            movementType = MovementType.TouchpadWalking;
        }
    }

    public void SwapHands()
    {
        Transform leftHandModel = leftHand.transform.GetChild(0);

        rightHand.transform.GetChild(0).SetParent(leftHand.transform);
        leftHandModel.SetParent(rightHand.transform);

        leftHand.transform.GetChild(0).transform.localPosition = handBasePos;
        leftHand.transform.GetChild(0).transform.localRotation = Quaternion.Euler(Vector3.zero);
        rightHand.transform.GetChild(0).transform.localPosition = handBasePos;
        rightHand.transform.GetChild(0).transform.localRotation = Quaternion.Euler(Vector3.zero);

        leftHand.Awake();
        rightHand.Awake();
    }

    private void SetupEditorSymbols()
    {
        string definesString = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
        List<string> allDefines = definesString.Split(';').ToList();
        bool containsTestSymbol = allDefines.Contains("TEST");

        if (editorTesting)
        {
            if (!containsTestSymbol)
            {
                allDefines.Add("TEST");
                PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, string.Join(";", allDefines.ToArray()));
            }
        }
        else
        {
            if (containsTestSymbol)
            {
                allDefines.Remove("TEST");
                PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, string.Join(";", allDefines.ToArray()));
            }
        }
    }
}
