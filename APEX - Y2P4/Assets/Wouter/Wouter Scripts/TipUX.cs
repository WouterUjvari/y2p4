using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipUX : MonoBehaviour {

    public LineRenderer myLine;

    [Header("Line transforms")]
    public Transform origin;
    public Transform hook;
    public Transform target;
    public string targetToFind;

    [Header("Rotate")]
    public Transform targetCam;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    public void Start()
    {
        if(targetToFind != null)
        {
            target = GameObject.Find(targetToFind).transform;
        }     
    }

    public void FixedUpdate()
    {
        myLine.SetPosition(0, origin.position);
        myLine.SetPosition(1, hook.position);
        myLine.SetPosition(2, target.position);
    }

    public void LateUpdate()
    {
        Vector3 desiredPosition = targetCam.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(targetCam);
    }
}
