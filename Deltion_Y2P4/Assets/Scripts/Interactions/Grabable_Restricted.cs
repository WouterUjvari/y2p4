﻿using UnityEngine;

public class Grabable_Restricted : Grabable 
{

    private bool restrict;

    public enum AxisToRestrict
    {
        X,
        XY,
        XZ,
        Y,
        YZ,
        Z
    }
    public AxisToRestrict axisToRestrict;

    private Vector3 restrictedAxis;
    private Quaternion restrictedRot;

    private Vector3 defaultPos;

    [SerializeField]
    private float maxMoveAmount = 5f;

    private VRInteractor interactingHand;
    private Vector3 interactingHandPos;

    public override void Awake()
    {
        base.Awake();

        defaultPos = transform.localPosition;
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.C))
        //{
        //    Interact(testHand);
        //}
        //if (Input.GetKeyDown(KeyCode.V))
        //{
        //    DeInteract(testHand);
        //}

        if (!restrict)
        {
            return;
        }

		Vector3 deltaPos = interactingHand.transform.position - interactingHandPos;
        interactingHandPos = interactingHand.transform.position;

        switch (axisToRestrict)
        {
            case AxisToRestrict.X:

                //transform.position += new Vector3(0, deltaPos.y, deltaPos.z);
                transform.localPosition = new Vector3(transform.localPosition.x, GetClampedAxis(transform.localPosition.y + deltaPos.y, "y"), GetClampedAxis(transform.localPosition.z + deltaPos.z, "z"));
                break;
            case AxisToRestrict.XY:

                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, GetClampedAxis(transform.localPosition.z + deltaPos.z, "z"));
                break;
            case AxisToRestrict.XZ:

                transform.localPosition = new Vector3(transform.localPosition.x, GetClampedAxis(transform.localPosition.y + deltaPos.y, "y"), transform.localPosition.z);
                break;
            case AxisToRestrict.Y:

                transform.localPosition = new Vector3(GetClampedAxis(transform.localPosition.x + deltaPos.x, "x"), transform.localPosition.y, GetClampedAxis(transform.localPosition.z + deltaPos.z, "z"));
                break;
            case AxisToRestrict.YZ:

                transform.localPosition = new Vector3(GetClampedAxis(transform.localPosition.x + deltaPos.x, "x"), transform.localPosition.y, transform.localPosition.z);
                break;
            case AxisToRestrict.Z:

                transform.localPosition = new Vector3(GetClampedAxis(transform.localPosition.x + deltaPos.x, "x"), GetClampedAxis(transform.localPosition.y + deltaPos.y, "y"), transform.localPosition.z);
                break;
        }

        transform.rotation = restrictedRot;
    }
		
    public override void Interact(VRInteractor hand)
    {
        Grab(hand);
    }

    public override void DeInteract(VRInteractor hand)
    {
        Release(hand);
    }

    public override void Grab(VRInteractor hand)
    {
        interactingHand = hand;
        interactingHandPos = hand.transform.position;

        restrict = true;
        LockRestrictedAxis();
    }

    public override void Release(VRInteractor hand)
    {
        restrict = false;
        interactingHand = null;
    }

    private void LockRestrictedAxis()
    {
        restrictedAxis = transform.position;
        restrictedRot = transform.rotation;
    }

    private void ResetRigidbodyConstraints()
    {
        rb.constraints = RigidbodyConstraints.None;
    }

    private float GetClampedAxis(float axisVar, string axisName)
    {
        float f = axisVar;

        switch (axisName)
        {
            case "x":

                f = Mathf.Clamp(f, defaultPos.x - maxMoveAmount, defaultPos.x + maxMoveAmount);
                break;
            case "y":

                f = Mathf.Clamp(f, defaultPos.y - maxMoveAmount, defaultPos.y + maxMoveAmount);
                break;
            case "z":

                f = Mathf.Clamp(f, defaultPos.z - maxMoveAmount, defaultPos.z + maxMoveAmount);
                break;
        }

        return f;
    }
}
