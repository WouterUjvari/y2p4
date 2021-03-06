﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{

    private enum State
    {
        Patroling,
        Idling,
        GrabbedByPlayer,
        Stunned,
        LookAtPlayer
    }
    [SerializeField]
    private State state;
    private State? bufferState = null;
    private State? beforeGrabState = null;

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
    [SerializeField]
    private GameObject brokenDroneBaseBody;

    public bool isBroken = true;
    private Vector3 destination = Vector3.zero;
    private Rigidbody rb;
    private float stunnedAfterGrabCooldown;
    private ObjectSnapper objSnapper;
    private Animator droneAnim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        maximumHeight = (maximumHeight < minumumHeight) ? minumumHeight + 1 : maximumHeight;
        objSnapper = GetComponent<ObjectSnapper>();
    }

    private void Update()
    {
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

                Stunned();
                break;
            case State.LookAtPlayer:

                LookAtPlayer();
                break;
        }

        if (canStabilize)
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, transform.localEulerAngles.y, 0), Time.deltaTime * stabilizeSpeed);
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
                Vector3 newDir = Vector3.RotateTowards(transform.forward, -targetDir, Time.deltaTime * rotateSpeed, 0.0f);
                transform.rotation = Quaternion.Euler(transform.localEulerAngles.x, Quaternion.LookRotation(newDir).eulerAngles.y, transform.localEulerAngles.z);

                if (Physics.Linecast(transform.position, destination))
                {
                    destination = GetDestination(transform, moveUpdateRadius);
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
            destination = GetDestination(transform, moveUpdateRadius);
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

    private void Stunned()
    {
        if (stunnedAfterGrabCooldown > 0)
        {
            stunnedAfterGrabCooldown -= Time.deltaTime;
        }
        else
        {
            EndStun();
        }
    }

    private void LookAtPlayer()
    {
        if (Vector3.Distance(destination, transform.position) > stoppingDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * moveSpeed);

            Vector3 targetDir = destination - transform.position;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, -targetDir, Time.deltaTime * rotateSpeed, 0.0f);
            transform.rotation = Quaternion.Euler(transform.localEulerAngles.x, Quaternion.LookRotation(newDir).eulerAngles.y, transform.localEulerAngles.z);
        }
        else
        {
            Vector3 targetDir = VRPlayerMovementManager.instance.headTransform.position - transform.position;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, -targetDir, Time.deltaTime * rotateSpeed, 0.0f);
            transform.rotation = Quaternion.Euler(transform.localEulerAngles.x, Quaternion.LookRotation(newDir).eulerAngles.y, transform.localEulerAngles.z);
        }
    }

    public void GetNewState()
    {
        float f = Random.value;
        State newState;

        if (f <= 0.33f)
        {
            currentStopTime = Random.Range((float)(0.75 * stopTime), (float)(1.25 * stopTime));
            newState = State.Idling;
        }
        else
        {
            newState = State.Patroling;
        }

        if (state == State.Stunned)
        {
            bufferState = newState;
            return;
        }

        state = (bufferState != null) ? (State)bufferState : newState;
        bufferState = null;
    }

    private Vector3 GetDestination(Transform center, float radius)
    {
        Vector3 validDestination = Vector3.zero;
        int tries = 0;

        while (validDestination == Vector3.zero)
        {
            Vector3 newDestination = (Random.insideUnitSphere * radius) + center.position;
            tries++;

            if (newDestination.y >= minumumHeight && newDestination.y <= maximumHeight)
            {
                if (!Physics.Linecast(transform.position, newDestination) || tries > 500)
                {
                    validDestination = newDestination;
                }
            }
        }

        if (tries > 100)
        {
            print("Drone found new destination in " + tries + " tries.");
        }
        return validDestination;
    }

    private void EndStun()
    {
        rb.useGravity = false;
        rb.isKinematic = true;

        if (beforeGrabState != null)
        {
            state = (State)beforeGrabState;
        }
        else if (bufferState != null)
        {
            state = (State)bufferState;
        }
        else
        {
            state = State.Patroling;
        }

        beforeGrabState = null;
    }

    public void DroneGrabbed()
    {
        beforeGrabState = state;
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
        Destroy(brokenDroneBaseBody);

        if (VRPlayerMovementManager.instance.leftHand.interactingObject == GetComponent<Interactable>())
        {
            VRPlayerMovementManager.instance.leftHand.DeInteract();
        }
        if (VRPlayerMovementManager.instance.rightHand.interactingObject == GetComponent<Interactable>())
        {
            VRPlayerMovementManager.instance.rightHand.DeInteract();
        }

        GameObject drone = Instantiate(repairedDrone, repairedDroneSpawn.position, repairedDroneSpawn.rotation, repairedDroneSpawn);
        droneAnim = drone.GetComponent<Animator>();
        if (droneAnim != null)
        {
            droneAnim.SetTrigger("Search");
        }

        rb.useGravity = false;
        rb.isKinematic = true;

        isBroken = false;
        canStabilize = true;
        FlowManager.instance.DroneBuildEvent();
    }

    public void GoLookAtPlayer()
    {
        state = State.LookAtPlayer;
        destination = GetDestination(VRPlayerMovementManager.instance.headTransform, 2);
    }
}
