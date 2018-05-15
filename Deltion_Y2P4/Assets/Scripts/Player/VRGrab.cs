using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRGrab : MonoBehaviour
{

    private SteamVR_TrackedObject trackedObj;

    private GameObject collidingObject;
    private GameObject objectInHand;

	[SerializeField]
	private VRGrab otherHand;

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

    public void OnTriggerExit(Collider other)
    {
        if (!collidingObject)
        {
            return;
        }

        Highlightable interactable = other.GetComponent<Highlightable>();
        if (interactable != null)
        {
            interactable.DeHighlight();
        }

        collidingObject = null;
    }

    private void SetCollidingObject(Collider col)
    {
        if (collidingObject || !col.GetComponent<Rigidbody>())
        {
            return;
        }

        if (otherHand != null)
        {
            if (otherHand.objectInHand != collidingObject)
            {
                Highlightable highlightable = col.GetComponent<Highlightable>();
                if (highlightable != null)
                {
                    highlightable.Highlight();
                }
            }
        }


        collidingObject = col.gameObject;
    }

    private void GrabObject()
    {
		if (otherHand != null) 
		{
			if (otherHand.objectInHand == collidingObject) 
			{
				otherHand.ReleaseObject();
			}
		}

        Highlightable highlightable = collidingObject.GetComponent<Highlightable>();
        if (highlightable != null)
        {
            highlightable.DeHighlight();
        }

        objectInHand = collidingObject;
        collidingObject = null;

		objectInHand.transform.SetParent(this.transform);
		objectInHand.GetComponent<Rigidbody> ().useGravity = false;

        FixedJoint joint = AddFixedJoint();
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
    }

    private FixedJoint AddFixedJoint()
    {
        FixedJoint joint = gameObject.AddComponent<FixedJoint>();

        joint.breakForce = Mathf.Infinity;
		joint.breakTorque = Mathf.Infinity;

        return joint;
    }

    public void ReleaseObject()
    {
        FixedJoint joint = GetComponent<FixedJoint>();

        if (joint != null)
        {
            joint.connectedBody = null;
            Destroy(joint);

            Rigidbody rb = objectInHand.GetComponent<Rigidbody>();
            rb.velocity = Controller.velocity;
            rb.angularVelocity = Controller.angularVelocity;
			rb.useGravity = true;
        }

		objectInHand.transform.SetParent(null);
        objectInHand = null;
    }
}
