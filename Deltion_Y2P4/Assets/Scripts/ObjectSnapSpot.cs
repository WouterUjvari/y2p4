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
        snappedObject = obj;

        state = State.Taken;
    }

    public void UnSnapObject()
    {
        if (state != State.Taken)
        {
            return;
        }

        snappedObject = null;

        state = State.Available;
    }
}
