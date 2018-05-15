using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Grabable : Interactable
{

    [SerializeField]
    private bool reparent;

    private Rigidbody rb;
    private Transform originalParent;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        originalParent = transform.parent;
    }

    public override void Interact(VRInteractor hand)
    {
        Grab(hand);
    }

    public override void DeInteract(VRInteractor hand)
    {
        Release(hand);
    }

    private void Grab(VRInteractor hand)
    {
        AddFixedJoint(hand);
        transform.SetParent(reparent ? hand.transform : transform.parent);
        rb.useGravity = false;
    }

    private void Release(VRInteractor hand)
    {
        DestroyFixedJoint(hand);
        transform.SetParent(reparent ? originalParent : transform.parent);
        rb.useGravity = true;
    }

    private void AddFixedJoint(VRInteractor hand)
    {
        FixedJoint joint = hand.gameObject.AddComponent<FixedJoint>();

        joint.breakForce = Mathf.Infinity;
        joint.breakTorque = Mathf.Infinity;

        joint.connectedBody = rb;
    }

    private void DestroyFixedJoint(VRInteractor hand)
    {
        FixedJoint joint = hand.GetComponent<FixedJoint>();

        if (joint != null)
        {
            //joint.connectedBody = null;
            Destroy(joint);

            rb.velocity = hand.Controller.velocity;
            rb.angularVelocity = hand.Controller.angularVelocity;
        }
    }
}
