using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRGrab : MonoBehaviour
{

    private SteamVR_TrackedObject trackedObj;

    private GameObject collidingObject;
    private GameObject objectInHand;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    private void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    private void Update()
    {
        if (Controller.GetHairTriggerDown())
        {
            if (collidingObject)
            {
                GrabObject();
            }
        }

        if (Controller.GetHairTriggerUp())
        {
            if (objectInHand)
            {
                ReleaseObject();
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        SetCollidingObject(other);
    }

    public void OnTriggerStay(Collider other)
    {
        SetCollidingObject(other);
    }

    public void OnTriggerExit(Collider other)
    {
        if (!collidingObject)
        {
            return;
        }

        collidingObject = null;
    }

    private void SetCollidingObject(Collider col)
    {
        if (collidingObject || !col.GetComponent<Rigidbody>())
        {
            return;
        }

        collidingObject = col.gameObject;
    }

    private void GrabObject()
    {
        objectInHand = collidingObject;
        collidingObject = null;

        FixedJoint joint = AddFixedJoint();
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
    }

    private FixedJoint AddFixedJoint()
    {
        FixedJoint joint = gameObject.AddComponent<FixedJoint>();

        joint.breakForce = 20000;
        joint.breakTorque = 20000;

        return joint;
    }

    private void ReleaseObject()
    {
        FixedJoint joint = GetComponent<FixedJoint>();

        if (joint != null)
        {
            joint.connectedBody = null;
            Destroy(joint);

            Rigidbody rb = objectInHand.GetComponent<Rigidbody>();
            rb.velocity = Controller.velocity;
            rb.angularVelocity = Controller.angularVelocity;
        }

        objectInHand = null;
    }
}
