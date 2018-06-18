using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : Interactable
{

    private bool rotate;

    [SerializeField]
    private bool invertRotation;

    [SerializeField]
    private Transform toRotate;
    private Transform rotator;
    private float rotatorLocalYRot;

    [Space(10)]

    [SerializeField]
    private float interactBreakDistance = 0.5f;

    [Header("Restrictions")]
    [SerializeField]
    private bool clampRotation;
    public float minRot;
    public float maxRot;

    private void Update()
    {
        if (!rotate)
        {
            return;
        }

        if (Vector3.Distance(interactingHand.transform.position, transform.position) > interactBreakDistance)
        {
            DeInteract(interactingHand);
            return;
        }

        float deltaRotatorLocalYRot = rotator.localEulerAngles.y - rotatorLocalYRot;
        rotatorLocalYRot = rotator.localEulerAngles.y;

        float newEulerZ = invertRotation ? toRotate.localEulerAngles.z + deltaRotatorLocalYRot : toRotate.localEulerAngles.z - deltaRotatorLocalYRot;
        newEulerZ = clampRotation ? newEulerZ.ClampAngle(minRot, maxRot) : newEulerZ;
        toRotate.localEulerAngles = new Vector3(toRotate.localEulerAngles.x, toRotate.localEulerAngles.y, newEulerZ);
    }

    public override void Interact(VRInteractor hand)
    {
        base.Interact(hand);

        interactingHand = hand;
        StartRotating(hand.transform);
    }

    public override void DeInteract(VRInteractor hand)
    {
        base.DeInteract(hand);

        interactingHand = null;
        StopRotating();
    }

    public void StartRotating(Transform t)
    {
        rotator = t;
        rotatorLocalYRot = t.localEulerAngles.y;
        rotate = true;
    }

    public void StopRotating()
    {
        rotate = false;
        rotator = null;
    }
}
