using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotatable : Interactable
{

    private enum Axis { X, Y, Z}
    [SerializeField]
    private Axis axis;

    private enum Type { Delta, LookRotation }
    [SerializeField]
    private Type type;

    [Space(10)]

    [SerializeField]
    private Transform objectToRotate;
    [SerializeField]
    private Transform interactPoint;

    [Space(10)]

    [Header("Delta Settings")]
    [SerializeField]
    private float rotateMultiplier = 200f;
    [SerializeField]
    private float eulerAnglesMax = 330;
    [Header("LookRotation Settings")]
    [SerializeField]
    private float minRot;
    [SerializeField]
    private float maxRot;

    [Space(10)]

    [SerializeField]
    private float interactBreakDistance = 0.5f;

    private Vector3 interactingHandPos;

    public VRInteractor testHand;

    private void Awake()
    {
        if (objectToRotate == null)
        {
            objectToRotate = transform;
        }
        if (interactPoint == null)
        {
            interactPoint = transform;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Interact(testHand);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            DeInteract(testHand);
        }

        if (interactingHand == null)
        {
            return;
        }

        if (Vector3.Distance(interactingHand.transform.position, interactPoint.position) > interactBreakDistance)
        {
            DeInteract(interactingHand);
            return;
        }

        if (type == Type.LookRotation)
        {
            Quaternion targetRot = Quaternion.LookRotation(interactingHand.transform.position - objectToRotate.transform.position);
            Vector3 newRot = objectToRotate.eulerAngles;

            switch (axis)
            {
                case Axis.X:

                    newRot.x = GetPositiveClampedAngle(targetRot.eulerAngles.x);
                    break;
                case Axis.Y:

                    newRot.y = GetPositiveClampedAngle(targetRot.eulerAngles.y);
                    break;
                case Axis.Z:

                    newRot.z = GetPositiveClampedAngle(targetRot.eulerAngles.z);
                    break;
            }

            objectToRotate.eulerAngles = newRot;
        }
        else if (type == Type.Delta)
        {
            Vector3 deltaPos = interactingHand.transform.position - interactingHandPos;
            interactingHandPos = interactingHand.transform.position;

            float deltaDistance = (deltaPos.x + deltaPos.y + deltaPos.z) / 3;

            if (deltaDistance != 0 && Mathf.Abs(deltaDistance) < 0.05f)
            {
                float rotationChange = (deltaDistance * Time.deltaTime) * rotateMultiplier;

                Quaternion currentRot = objectToRotate.localRotation;

                if (axis == Axis.X)
                {
                    Quaternion desiredRot = currentRot *= Quaternion.Euler(rotationChange * 100, 0, 0);

                    if (desiredRot.eulerAngles.x < eulerAnglesMax)
                    {
                        objectToRotate.localRotation *= Quaternion.Euler(rotationChange * 100, 0, 0);
                    }
                }
                else if (axis == Axis.Y)
                {
                    Quaternion desiredRot = currentRot *= Quaternion.Euler(0, rotationChange * 100, 0);

                    if (desiredRot.eulerAngles.y < eulerAnglesMax)
                    {
                        objectToRotate.localRotation *= Quaternion.Euler(0, rotationChange * 100, 0);
                    }
                }
            }
        }
    }

    public override void Interact(VRInteractor hand)
    {
        base.Interact(hand);

        interactingHand = hand;
        interactingHandPos = hand.transform.position;
    }

    public override void DeInteract(VRInteractor hand)
    {
        base.DeInteract(hand);

        interactingHand = null;
    }

    private float GetPositiveClampedAngle(float angle)
    {
        if (angle < 90 || angle > 270)
        {
            angle = (angle > 180) ? angle - 360 : angle;
            maxRot = (maxRot > 180) ? maxRot - 360 : maxRot;
            minRot = (minRot > 180) ? minRot - 360 : minRot;
        }

        if (minRot != 0 && maxRot != 0)
        {
            angle = Mathf.Clamp(angle, minRot, maxRot);
        }

        angle = (angle < 0) ? angle + 360 : angle;

        return angle;
    }
}
