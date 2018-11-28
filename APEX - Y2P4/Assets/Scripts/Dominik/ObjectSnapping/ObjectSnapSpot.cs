using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    public Transform desiredObject;
    [HideInInspector]
    public bool isLookingForSpecificObject;
    [SerializeField]
    private bool removeInteractionComponentsOnDesiredSnap;
    [SerializeField]
    private bool lockObjectOnSnap;
    [SerializeField]
    private UnityEvent onSnap;

    private void Awake()
    {
        if (snappedObject != null)
        {
            SnapObject(snappedObject);
        }

        isLookingForSpecificObject = (desiredObject != null) ? true : false;
    }

    public void SnapObject(Transform obj)
    {
        if (state != State.Available)
        {
            return;
        }

        if (isLookingForSpecificObject && obj != desiredObject)
        {
            return;
        }
        else
        {
            if (removeInteractionComponentsOnDesiredSnap)
            {
                RemoveInteractionComponents(obj);
            }
        }

        obj.transform.position = transform.position;
        obj.transform.rotation = transform.rotation;
        obj.SetParent(transform);

        Rigidbody rb = obj.GetComponent<Rigidbody>();
        snappedObjectKinematic = (rb.isKinematic) ? true : false;
        obj.GetComponent<Rigidbody>().isKinematic = true;

        snappedObject = obj;

        state = State.Taken;

        if (lockObjectOnSnap)
        {
            LockSnappedObject();
        }

        onSnap.Invoke();
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

    private void RemoveInteractionComponents(Transform obj)
    {
        Destroy(obj.GetComponent<Interactable>());
        Highlightable highlightable = obj.GetComponent<Highlightable>();
        if (highlightable != null)
        {
            Destroy(highlightable);
        }

        Collider[] colliders = obj.GetComponentsInChildren<Collider>();
        for (int i = 0; i < colliders.Length; i++)
        {
            Destroy(colliders[i]);
        }
    }

    public void LockSnappedObject()
    {
        snappedObject.GetComponent<Interactable>().Lock = true;
    }
}
