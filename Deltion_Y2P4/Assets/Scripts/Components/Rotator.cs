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

    public VRInteractor testHand;

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

        if (!rotate)
        {
            return;
        }

        float deltaRotatorLocalYRot = rotator.localEulerAngles.y - rotatorLocalYRot;
        rotatorLocalYRot = rotator.localEulerAngles.y;

        float newEulerZ = invertRotation ? toRotate.transform.eulerAngles.z - deltaRotatorLocalYRot : toRotate.transform.eulerAngles.z + deltaRotatorLocalYRot;
        toRotate.transform.eulerAngles = new Vector3(0, 0, newEulerZ);
    }

    public override void Interact(VRInteractor hand)
    {
        base.Interact(hand);

        StartRotating(hand.transform);
    }

    public override void DeInteract(VRInteractor hand)
    {
        base.DeInteract(hand);

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
