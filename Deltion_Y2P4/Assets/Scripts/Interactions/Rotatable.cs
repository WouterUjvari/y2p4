using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotatable : Interactable
{

    [SerializeField]
    private float rotateMultiplier = 200f;
    [SerializeField]
    private float minX;
    [SerializeField]
    private float maxX;

    private VRInteractor interactingHand;
    private Vector3 interactingHandPos;

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

        if (interactingHand == null)
        {
            return;
        }

        Vector3 deltaPos = interactingHand.transform.position - interactingHandPos;
        interactingHandPos = interactingHand.transform.position;

        if (deltaPos != Vector3.zero)
        {
            float rotationChange = deltaPos.z * Time.deltaTime * rotateMultiplier;
            transform.localRotation *= Quaternion.Euler(-rotationChange * 100, 0, 0);
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
