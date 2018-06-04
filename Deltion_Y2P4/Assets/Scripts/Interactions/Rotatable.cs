using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotatable : Interactable
{

    private enum Axis { X, Y, Z }
    [SerializeField]
    private Axis axis;
    [SerializeField]
    private Axis lookRotationLock;

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
    private bool clampAngle;
    [SerializeField]
    private float minRot;
    [SerializeField]
    private float maxRot;

    [Space(10)]

    [SerializeField]
    private float interactBreakDistance = 0.5f;

    private Vector3 interactingHandPos;
    private Vector3 startRot;
    private Vector3 lastValidRot;

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

        startRot = objectToRotate.localEulerAngles;
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
            Vector3 relativePos = interactingHand.transform.position - objectToRotate.position;
            switch (lookRotationLock)
            {
                case Axis.X:

                    relativePos.x = 0;
                    break;
                case Axis.Y:

                    relativePos.y = 0;
                    break;
                case Axis.Z:

                    relativePos.z = 0;
                    break;
            }

            Quaternion rotation = Quaternion.LookRotation(relativePos);
            Vector3 rotationEuler = rotation.eulerAngles;

            if (clampAngle)
            {
                switch (axis)
                {
                    case Axis.X:

                        rotationEuler.x = ClampAngle(rotationEuler.x, minRot, maxRot);
                        break;
                    case Axis.Y:

                        rotationEuler.y = ClampAngle(rotationEuler.y, minRot, maxRot);
                        break;
                    case Axis.Z:

                        rotationEuler.z = ClampAngle(rotationEuler.z, minRot, maxRot);
                        break;
                }
            }

            objectToRotate.eulerAngles = rotationEuler;
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
        if (locked)
        {
            return;
        }

        base.Interact(hand);

        interactingHand = hand;
        interactingHandPos = hand.transform.position;
    }

    public override void DeInteract(VRInteractor hand)
    {
        base.DeInteract(hand);

        interactingHand = null;
    }

    public float ClampAngle(float angle, float min, float max)
    {
        angle = Mathf.Repeat(angle, 360);
        min = Mathf.Repeat(min, 360);
        max = Mathf.Repeat(max, 360);

        bool inverse = false;
        float tmin = min;
        float tangle = angle;
        if (min > 180)
        {
            inverse = !inverse;
            tmin -= 180;
        }
        if (angle > 180)
        {
            inverse = !inverse;
            tangle -= 180;
        }
        bool result = !inverse ? tangle > tmin : tangle < tmin;
        if (!result)
        {
            angle = min;
        }

        inverse = false;
        tangle = angle;
        float tmax = max;
        if (angle > 180)
        {
            inverse = !inverse;
            tangle -= 180;
        }
        if (max > 180)
        {
            inverse = !inverse;
            tmax -= 180;
        }

        result = !inverse ? tangle < tmax : tangle > tmax;
        if (!result)
        {
            angle = max;
        }

        return angle;
    }
}
