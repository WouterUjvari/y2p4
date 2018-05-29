using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotatable : Interactable
{

    private enum Axis
    {
        X,
        Y
    }
    [SerializeField]
    private Axis axis;
    [SerializeField]
    private Transform objectToRotate;
    [SerializeField]
    private Transform interactPoint;

    [Space(10)]

    [SerializeField]
    private float rotateMultiplier = 200f;
    [SerializeField]
    private float minX;
    [SerializeField]
    private float maxX;
    [SerializeField]
    private float eulerAnglesMax = 330;
    [SerializeField]
    private float interactBreakDistance = 0.5f;

    private VRInteractor interactingHand;
    private Vector3 interactingHandPos;

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
        //if (Input.GetKeyDown(KeyCode.C))
        //{
        //    Interact(testHand);
        //}
        //if (Input.GetKeyDown(KeyCode.V))
        //{
        //    DeInteract(testHand);
        //}

        if (interactingHand == null)
        {
            return;
        }

        if (Vector3.Distance(interactingHand.transform.position, interactPoint.position) > interactBreakDistance)
        {
            DeInteract(interactingHand);
            return;
        }

        Vector3 deltaPos = interactingHand.transform.position - interactingHandPos;
        interactingHandPos = interactingHand.transform.position;

        float deltaDistance = (deltaPos.x + deltaPos.y + deltaPos.z) / 3;

        if (deltaDistance != 0 && Mathf.Abs(deltaPos.z) < 0.085f)
        {
            float rotationChange = deltaDistance * Time.deltaTime * rotateMultiplier;

            Quaternion currentRot = objectToRotate.localRotation;

            if (axis == Axis.X)
            {
                Quaternion desiredRot = currentRot *= Quaternion.Euler(-rotationChange * 100, 0, 0);

                if (desiredRot.eulerAngles.x < eulerAnglesMax)
                {
                    objectToRotate.localRotation *= Quaternion.Euler(-rotationChange * 100, 0, 0);
                }
            }
            else if (axis == Axis.Y)
            {
                Quaternion desiredRot = currentRot *= Quaternion.Euler(0, -rotationChange * 100, 0);

                if (desiredRot.eulerAngles.y < eulerAnglesMax)
                {
                    objectToRotate.localRotation *= Quaternion.Euler(0, -rotationChange * 100, 0);
                }
            }
        }
    }

    public override void Interact(VRInteractor hand)
    {
        interactingHand = hand;
        interactingHandPos = hand.transform.position;
    }

    public override void DeInteract(VRInteractor hand)
    {
        interactingHand = null;
    }
}
