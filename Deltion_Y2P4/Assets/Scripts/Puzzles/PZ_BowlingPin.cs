using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PZ_BowlingPin : MonoBehaviour 
{

    public bool isActive;

    [SerializeField]
    private GameObject graphic;

    [SerializeField]
    private Material dissolveMat;

    [SerializeField]
    private float dissolveSpeed = 1f;

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

        StopCoroutine(DissolveEffect());
        StartCoroutine(DissolveEffect());

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

    private IEnumerator DissolveEffect()
    {
        dissolveMat.SetFloat("Vector1_643BC525", 0);
        while (dissolveMat.GetFloat("Vector1_643BC525") < 1)
        {
            dissolveMat.SetFloat("Vector1_643BC525", dissolveMat.GetFloat("Vector1_643BC525") + (Time.deltaTime * dissolveSpeed));
            yield return null;
        }
    }
}
