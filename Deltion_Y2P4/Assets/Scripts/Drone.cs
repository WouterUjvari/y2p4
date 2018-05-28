using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{

    private enum State
    {
        Patroling,
        Idling,
        GrabbedByPlayer
    }
    [SerializeField]
    private State state;

    [SerializeField]
    private float moveSpeed = 1f;
    [SerializeField]
    private float rotateSpeed = 1f;
    [SerializeField]
    private float moveUpdateRadius;
    [SerializeField]
    private float stoppingDistance = 0.5f;
    [SerializeField]
    private float stopTime = 2f;
    private float currentStopTime;

    private Vector3 destination = Vector3.zero;

    private void Update()
    {
        switch (state)
        {
            case State.Patroling:

                Patrol();
                break;
            case State.Idling:

                Idle();
                break;
        }
    }

    private void Patrol()
    {
        if (destination != Vector3.zero)
        {
            Debug.DrawLine(transform.position, destination, Color.green);

            if (Vector3.Distance(destination, transform.position) > stoppingDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * moveSpeed);

                Vector3 targetDir = destination - transform.position;
                Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, Time.deltaTime * rotateSpeed, 0.0f);
                transform.rotation = Quaternion.Euler(0, Quaternion.LookRotation(newDir).eulerAngles.y, 0);

                if (Physics.Linecast(transform.position, destination))
                {
                    destination = GetDestination();
                }
            }
            else
            {
                destination = Vector3.zero;
                GetNewState();
            }
        }
        else
        {
            destination = GetDestination();
        }
    }

    private void Idle()
    {
        if (currentStopTime > 0)
        {
            currentStopTime -= Time.deltaTime;
        }
        else
        {
            GetNewState();
        }
    }

    private void GetNewState()
    {
        float f = Random.value;

        if (f <= 0.33f)
        {
            currentStopTime = Random.Range((float)(0.75 * stopTime), (float)(1.25 * stopTime));
            state = State.Idling;
        }
        else
        {
            state = State.Patroling;
        }
    }

    private Vector3 GetDestination()
    {
        Vector3 validDestination = Vector3.zero;

        while (validDestination == Vector3.zero)
        {
            Vector3 newDestination = Random.insideUnitSphere * moveUpdateRadius;

            if (!Physics.Linecast(transform.position, newDestination))
            {
                validDestination = newDestination;
            }
        }

        return validDestination;
    }

    public void DroneGrabbed()
    {
        state = State.GrabbedByPlayer;
    }

    public void DroneReleased()
    {
        state = State.Patroling;
    }
}
