using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartWatch : MonoBehaviour {

    public bool enableWaypoint;
    public GameObject waypointArrow;
    public Transform target;

    void FixedUpdate()
    {
        waypointArrow.SetActive(enableWaypoint);
        if(enableWaypoint)
        {
            waypointArrow.transform.LookAt(target);
        }
    }
}
