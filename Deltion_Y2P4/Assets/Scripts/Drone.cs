using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{

    private enum State
    {
        Patroling,
        Idling,
        GrabbedByPlayer,
        Stunned
    }
    [SerializeField]
    private State state;

    [Header("Movement")]
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

    [Space(10)]

    [SerializeField]
    private float minumumHeight;
    [SerializeField]
    private float maximumHeight;

    [Header("Stabilizing")]
    [SerializeField]
    private bool canStabilize;
    [SerializeField]
    private float stabilizeSpeed = 2f;
    [SerializeField]
    private float stunnedAfterGrabTime = 1f;

    [Header("Creation")]
    [SerializeField]
    private GameObject repairedDrone;
    [SerializeField]
    private Transform repairedDroneSpawn;

    public bool isBroken = true;
    private Vector3 destination = Vector3.zero;
    private Rigidbody rb;
    private float stunnedAfterGrabCooldown;
    private ObjectSnapper objSnapper;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        maximumHeight = (maximumHeight < minumumHeight) ? minumumHeight + 1 : maximumHeight;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            DroneGrabbed();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            DroneReleased();
        }

        if (isBroken)
        {
            return;
        }

        switch (state)
        {
            case State.Patroling:

                Patrol();
                break;
            case State.Idling:

                Idle();
                break;
            case State.Stunned:

                if (stunnedAfterGrabCooldown > 0)
                {
                    stunnedAfterGrabCooldown -= Time.deltaTime;
                }
                else
                {
                    EndStun();
                }

                break;
        }

        if (canStabilize)
        {
            float rotX = transform.eulerAngles.x - transform.eulerAngles.x;
            float rotZ = transform.eulerAngles.z - transform.eulerAngles.z;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(rotX, 0, rotZ), Time.deltaTime * stabilizeSpeed);
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
                transform.rotation = Quaternion.Euler(transform.localEulerAngles.x, Quaternion.LookRotation(newDir).eulerAngles.y, transform.localEulerAngles.z);

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
            Vector3 newDestination = (Random.insideUnitSphere * moveUpdateRadius) + transform.position;

            if (newDestination.y >= minumumHeight && newDestination.y <= maximumHeight)
            {
                if (!Physics.Linecast(transform.position, newDestination))
                {
                    validDestination = newDestination;
                }
            }
        }

        return validDestination;
    }

    private void EndStun()
    {
        rb.useGravity = false;
        rb.isKinematic = true;
        state = State.Patroling;
    }

    public void DroneGrabbed()
    {
        state = State.GrabbedByPlayer;
        canStabilize = false;
    }

    public void DroneReleased()
    {
        stunnedAfterGrabCooldown = stunnedAfterGrabTime;
        state = State.Stunned;
        rb.useGravity = true;
        rb.isKinematic = false;
        canStabilize = true;
    }

    public void RepairDrone()
    {
        objSnapper.DestroySnappedObjects();
        Destroy(objSnapper);
        Instantiate(repairedDrone, repairedDroneSpawn.position, repairedDroneSpawn.rotation, repairedDroneSpawn);

        rb.useGravity = true;
        rb.isKinematic = false;

        isBroken = false;
        canStabilize = true;
    }
}
