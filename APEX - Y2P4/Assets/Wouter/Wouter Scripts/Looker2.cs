using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Looker2 : MonoBehaviour
{

    public Transform target;

    private void LateUpdate()
    {
        Vector3 relativePos = transform.position - target.position;
        relativePos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(relativePos);

        Vector3 rotationEuler = rotation.eulerAngles;
        transform.eulerAngles = rotationEuler;
    }
}
