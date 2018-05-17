using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Grabable : Interactable
{

    [SerializeField]
    private bool reparent;

    [SerializeField]
    private List<Collider> collidersToTurnOff = new List<Collider>();

    private Rigidbody rb;
    private Transform originalParent;

    private bool gravity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        originalParent = transform.parent;

        gravity = rb.useGravity;
    }

    public override void Interact(VRInteractor hand)
    {
        Grab(hand);
    }

    public override void DeInteract(VRInteractor hand)
    {
        Release(hand);
    }

    protected void Grab(VRInteractor hand)
    {
        AddFixedJoint(hand);
        transform.SetParent(reparent ? hand.transform : transform.parent);
        rb.useGravity = false;

        for (int i = 0; i < collidersToTurnOff.Count; i++)
        {
            collidersToTurnOff[i].enabled = false;
        }
    }

    protected void Release(VRInteractor hand)
    {
        transform.SetParent(reparent ? originalParent : transform.parent);
        rb.useGravity = gravity ? true : false;

        for (int i = 0; i < collidersToTurnOff.Count; i++)
        {
            collidersToTurnOff[i].enabled = true;
        }

        DestroyFixedJoint(hand);
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
            joint.connectedBody = null;
            Destroy(joint);

            rb.velocity = hand.Controller.velocity;
            rb.angularVelocity = hand.Controller.angularVelocity;
        }
    }
}
