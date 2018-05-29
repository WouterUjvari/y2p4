using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSnapSpot : MonoBehaviour 
{

    public enum State
    {
        Available,
        Taken
    }
    public State state;

    public Transform snappedObject;
    private bool snappedObjectKinematic;

    private void Awake()
    {
        if (snappedObject != null)
        {
            SnapObject(snappedObject);
        }
    }

    public void SnapObject(Transform obj)
    {
        if (state != State.Available)
        {
            return;
        }

        obj.transform.position = transform.position;

        Rigidbody rb = obj.GetComponent<Rigidbody>();
        snappedObjectKinematic = (rb.isKinematic) ? true : false;
        obj.GetComponent<Rigidbody>().isKinematic = true;

        snappedObject = obj;

        state = State.Taken;
    }

    public void UnSnapObject()
    {
        if (state != State.Taken)
        {
            return;
        }

        snappedObject.GetComponent<Rigidbody>().isKinematic = snappedObjectKinematic;
        snappedObject = null;

        state = State.Available;
    }
}
