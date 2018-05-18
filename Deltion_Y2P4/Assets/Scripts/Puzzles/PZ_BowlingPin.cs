using System.Collections.Generic;
using UnityEngine;

public class PZ_BowlingPin : MonoBehaviour 
{

    public bool isActive;

    [SerializeField]
    private GameObject graphic;

    private Collider[] myColliders;

    private Rigidbody rb;

    private void Awake()
    {
        myColliders = GetComponentsInChildren<Collider>();
        rb = GetComponent<Rigidbody>();

        DeActivate();
    }

    public void Activate()
    {
        graphic.SetActive(true);

        for (int i = 0; i < myColliders.Length; i++)
        {
            myColliders[i].enabled = true;
        }

        rb.useGravity = true;

        isActive = true;
    }

    public void DeActivate()
    {
        graphic.SetActive(false);

        for (int i = 0; i < myColliders.Length; i++)
        {
            myColliders[i].enabled = false;
        }

        rb.useGravity = false;

        isActive = false;
    }
}
