using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectSnapper : MonoBehaviour
{

    [SerializeField]
    private List<ObjectSnapSpot> snapSpots = new List<ObjectSnapSpot>();

    [SerializeField]
    private float snapRange = 0.2f;

    [SerializeField]
    private UnityEvent onAllCorrectObjectsSnapped;

    public void SnapObject(Transform obj)
    {
        ObjectSnapSpot closestSnapSpot = null;

        // Loop through snapspots.
        for (int i = 0; i < snapSpots.Count; i++)
        {
            // If obj is in range of current snapspot.
            if (Vector3.Distance(snapSpots[i].transform.position, obj.position) < snapRange)
            {
                // If current snapspot is looking for this obj, then this is the right snapspot, break out of the loop.
                if (snapSpots[i].isLookingForSpecificObject && obj == snapSpots[i].desiredObject)
                {
                    closestSnapSpot = snapSpots[i];
                    break;
                }
                // Else if snapspot is not looking for a specific obj.
                else if (!snapSpots[i].isLookingForSpecificObject)
                {
                    // If there isnt a closest snapspot yet, make this the one and continue the loop.
                    if (closestSnapSpot == null)
                    {
                        closestSnapSpot = snapSpots[i];
                    }
                    else
                    {
                        // If there is already a closest snapspot, check if the current snapspot is closer to the object, if it is, make this the new closest snapspot.
                        if (Vector3.Distance(snapSpots[i].transform.position, obj.position) < Vector3.Distance(closestSnapSpot.transform.position, obj.position))
                        {
                            closestSnapSpot = snapSpots[i];
                        }
                    }
                }
            }
        }

        if (closestSnapSpot != null)
        {
            if (closestSnapSpot.state == ObjectSnapSpot.State.Available)
            {
                closestSnapSpot.SnapObject(obj);

                if (closestSnapSpot.isLookingForSpecificObject && obj == closestSnapSpot.desiredObject)
                {
                    obj.transform.SetParent(closestSnapSpot.transform);
                }
            }
        }

        if (AreAllSpecificObjectsCorrectlySnapped() == true)
        {
            onAllCorrectObjectsSnapped.Invoke();
        }
    }

    public void UnSnapObject(Transform obj)
    {
        for (int i = 0; i < snapSpots.Count; i++)
        {
            if (snapSpots[i].snappedObject == obj)
            {
                snapSpots[i].UnSnapObject();
            }
        }
    }

    private bool AreAllSpecificObjectsCorrectlySnapped()
    {
        for (int i = 0; i < snapSpots.Count; i++)
        {
            if (snapSpots[i].isLookingForSpecificObject)
            {
                if (snapSpots[i].snappedObject != snapSpots[i].desiredObject)
                {
                    return false;
                }
                else
                {
                    if (i == snapSpots.Count - 1)
                    {
                        if (snapSpots[i].snappedObject == snapSpots[i].desiredObject)
                        {
                            return true;
                        }
                    }
                }
            }
        }

        return false;
    }

    public void DestroySnappedObjects()
    {
        for (int i = 0; i < snapSpots.Count; i++)
        {
            if (snapSpots[i].snappedObject != null)
            {
                if (VRPlayerMovementManager.instance.leftHand.interactingObject == snapSpots[i].snappedObject)
                {
                    VRPlayerMovementManager.instance.leftHand.DeInteract();
                }
                if (VRPlayerMovementManager.instance.rightHand.interactingObject == snapSpots[i].snappedObject)
                {
                    VRPlayerMovementManager.instance.rightHand.DeInteract();
                }

                Destroy(snapSpots[i].snappedObject.gameObject);
            }
        }
    }
}
