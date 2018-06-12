using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PZ_BowlingPin : MonoBehaviour 
{

    public bool isActive;
    [HideInInspector]
    public float activeTime;
    private float currentActiveTime;

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
        dissolveMat = Instantiate(dissolveMat);

        Renderer[] myRenderers = GetComponentsInChildren<Renderer>();
        for (int i = 0; i < myRenderers.Length; i++)
        {
            myRenderers[i].material = dissolveMat;
        }

        myColliders = GetComponentsInChildren<Collider>();
        rb = GetComponent<Rigidbody>();

        DeActivate();
    }

    private void Update()
    {
        if (currentActiveTime <= 0 && isActive)
        {
            DeActivate();
        }
        else
        {
            currentActiveTime -= Time.deltaTime;
        }
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

        currentActiveTime = Random.Range((float)(0.75 * activeTime), (float)(1.25 * activeTime));
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
