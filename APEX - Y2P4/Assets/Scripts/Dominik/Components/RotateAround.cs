﻿using UnityEngine;

public class RotateAround : MonoBehaviour 
{

    public bool canRotate = true;
    private bool lockRotation;

    private enum Axis { X, Y, Z}
    [SerializeField]
    private Axis axis;
    [SerializeField]
    private float speed;

    private void Update()
    {
        if (canRotate)
        {
            switch (axis)
            {
                case Axis.X:

                    transform.Rotate(Time.deltaTime * speed, 0, 0);
                    break;
                case Axis.Y:

                    transform.Rotate(0, Time.deltaTime * speed, 0);
                    break;
                case Axis.Z:

                    transform.Rotate(0, 0, Time.deltaTime * speed);
                    break;
            }
        }
        else
        {
            if (lockRotation)
            {
                transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.Euler(transform.localRotation.x, -90, transform.localRotation.z), Time.deltaTime * (speed * 3));
            }
        }
    }

    public void SetRotation(bool b)
    {
        canRotate = b;
    }

    public void CheckChildRA()
    {
        RotateAround[] ra = GetComponentsInChildren<RotateAround>();
        for (int i = 0; i < ra.Length; i++)
        {
            if (ra[i] != this)
            {
                ra[i].canRotate = true;
            }
        }
    }

    public void LockRotation()
    {
        canRotate = false;
        lockRotation = true;
    }
}
