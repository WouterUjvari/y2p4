using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{

    [SerializeField]
    private float moveSpeed = 1f;
    [SerializeField]
    private float rotateSpeed = 1f;
    [SerializeField]
    private float moveUpdateRadius;
    [SerializeField]
    private float stoppingDistance = 0.5f;

    private Vector3 destination;

    private void Update()
    {
        if (destination != null)
        {
            if (Vector3.Distance(destination, transform.position) > stoppingDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * moveSpeed);

                Vector3 targetDir = destination - transform.position;
                Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, Time.deltaTime * rotateSpeed, 0.0f);
                transform.rotation = Quaternion.Euler(0, Quaternion.LookRotation(newDir).eulerAngles.y, 0);
            }
            else
            {
                destination = GetDestination();
            }
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
}
