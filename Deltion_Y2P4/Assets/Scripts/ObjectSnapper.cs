using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSnapper : MonoBehaviour
{

    [SerializeField]
    private List<ObjectSnapSpot> snapSpots = new List<ObjectSnapSpot>();

    [SerializeField]
    private float snapRange = 0.2f;

    public void SnapObject(Transform obj)
    {
        ObjectSnapSpot closestSnapSpot = null;

        for (int i = 0; i < snapSpots.Count; i++)
        {
            if (Vector3.Distance(snapSpots[i].transform.position, obj.position) < snapRange)
            {
                closestSnapSpot = snapSpots[i];
            }
        }

        if (closestSnapSpot != null)
        {
            if (closestSnapSpot.state == ObjectSnapSpot.State.Available)
            {
                closestSnapSpot.SnapObject(obj);
            }
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
}
