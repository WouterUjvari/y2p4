using UnityEngine;
using System.Collections;

// @robotduck 2011
// set the object's rigidbody angular drag to a high value, like 10

public class Looker : MonoBehaviour
{

    public Transform target;
    public float force = 0.1f;
    public Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {

        Vector3 targetDelta = target.position - transform.position;

        //get the angle between transform.forward and target delta
        float angleDiff = Vector3.Angle(transform.forward, targetDelta);

        // get its cross product, which is the axis of rotation to
        // get from one vector to the other
        Vector3 cross = Vector3.Cross(transform.forward, targetDelta);

        // apply torque along that axis according to the magnitude of the angle.
        rb.AddTorque(cross * angleDiff * force);
    }
}