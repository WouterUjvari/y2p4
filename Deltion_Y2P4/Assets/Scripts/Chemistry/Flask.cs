using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flask : MonoBehaviour
{

    private enum Colors
    {
        Blue,
        Red,
        Magenta,
        Cyan,
        Green,
        Yellow
    }
    [SerializeField]
    private Colors colors;

    [SerializeField]
    private Color liquidColor;
    private Color myColor;

    [SerializeField]
    private ParticleSystem liquidParticle;

    [SerializeField]
    private ParticleSystem bubbleParticle;

    [SerializeField]
    private Transform staticLiquid;

    [SerializeField]
    private float staticLiquidFlowSpeed = 0.5f;

    [SerializeField]
    private float liquidColorLerpSpeed = 1f;

    private Renderer staticLiquidRenderer;
    private ParticleSystem.MainModule liquidModule;
    private ParticleSystem.EmissionModule liquidEmissionModule;
    private ParticleSystem.MainModule bubbleModule;

    private bool isLerpingColor;

    private void Awake()
    {
        staticLiquidRenderer = staticLiquid.GetChild(0).GetComponent<Renderer>();
        staticLiquidRenderer.material.SetColor("_Color", myColor);

        liquidModule = liquidParticle.main;
        liquidEmissionModule = liquidParticle.emission;
        liquidModule.startColor = myColor;

        bubbleModule = bubbleParticle.main;
        bubbleModule.startColor = myColor;
    }

    private void Update()
    {
        if (Vector3.Dot(transform.up, Vector3.down) > 0)
        {
            if (staticLiquid.localScale.z > 0)
            {
                liquidEmissionModule.rateOverTime = 500;
                staticLiquid.localScale -= new Vector3(0, 0, Time.deltaTime * staticLiquidFlowSpeed);
            }
            else
            {
                liquidEmissionModule.rateOverTime = 0;
                staticLiquid.localScale = new Vector3(1, 1, 0);
            }
        }
        else
        {
            liquidEmissionModule.rateOverTime = 0;
        }

        if (staticLiquid.localScale.z > 0)
        {
            if (!bubbleParticle.isPlaying)
            {
                bubbleParticle.Play();
            }
        }
        else
        {
            if (!bubbleParticle.isStopped)
            {
                bubbleParticle.Stop();
            }
        }
    }

    public void AddLiquid(Color color)
    {
        if (staticLiquid.localScale.z < 1)
        {
            staticLiquid.localScale += new Vector3(0, 0, Time.deltaTime * staticLiquidFlowSpeed);
        }

        if (!isLerpingColor && color != myColor)
        {
            Color newColor = (myColor + color) / 2;
            StartCoroutine(LerpColor(myColor, newColor));
        }
    }

    private IEnumerator LerpColor(Color original, Color newColor)
    {
        isLerpingColor = true;
        float lerpTime = 0;

        while (staticLiquidRenderer.material.color != newColor)
        {
            staticLiquidRenderer.material.color = Color.Lerp(original, newColor, lerpTime);
            liquidModule.startColor = Color.Lerp(original, newColor, lerpTime);
            bubbleModule.startColor = Color.Lerp(original, newColor, lerpTime);

            lerpTime += Time.deltaTime * liquidColorLerpSpeed;
            yield return null;
        }
        myColor = newColor;

        isLerpingColor = false;
    }
}
