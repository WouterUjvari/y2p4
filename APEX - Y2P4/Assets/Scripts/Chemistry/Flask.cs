using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flask : MonoBehaviour
{

    [SerializeField]
    private string myColorName;
    [SerializeField]
    private Liquid myLiquid;

    [Space(10)]

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
    [SerializeField]
    private Transform liquidEntrancePos;
    [SerializeField]
    private float liquidDestroyAtEntranceRange = 0.1f;

    private Color myCurrentColor;
    private Renderer staticLiquidRenderer;
    private ParticleSystem.MainModule liquidModule;
    private ParticleSystem.EmissionModule liquidEmissionModule;
    private ParticleSystem.MainModule bubbleModule;

    private bool isLerpingColor;

    private Vector3 emptyLiquidScale = new Vector3(1, 1, 0);

    private void Awake()
    {
        ColorMixingManager.Colors myColor = ColorMixingManager.instance.GetColorByName(myColorName);
        myCurrentColor = myColor.color;
        myLiquid.myColor = myCurrentColor;

        staticLiquidRenderer = staticLiquid.GetChild(0).GetComponent<Renderer>();
        staticLiquidRenderer.material.SetColor("_Color", myCurrentColor);

        liquidModule = liquidParticle.main;
        liquidEmissionModule = liquidParticle.emission;
        liquidModule.startColor = myCurrentColor;

        bubbleModule = bubbleParticle.main;
        bubbleModule.startColor = myCurrentColor;
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
                staticLiquid.localScale = emptyLiquidScale;
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
        if (!isLerpingColor && color != myCurrentColor)
        {
            Color newColor = (staticLiquid.localScale.z == 0) ? myCurrentColor : ColorMixingManager.instance.GetMixedColor(myColorName, color);
            StartCoroutine(LerpColor(myCurrentColor, newColor));
        }

        if (staticLiquid.localScale.z < 1)
        {
            staticLiquid.localScale += new Vector3(0, 0, Time.deltaTime * staticLiquidFlowSpeed);
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
        myCurrentColor = newColor;
        myLiquid.myColor = newColor;

        isLerpingColor = false;
    }

    public void DestroyNearbyParticles(ParticleSystem pSystem)
    {
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[pSystem.main.maxParticles];
        int aliveParticles = pSystem.GetParticles(particles);

        for (int i = 0; i < aliveParticles; i++)
        {
            if (Vector3.Distance(particles[i].position, liquidEntrancePos.position) < liquidDestroyAtEntranceRange)
            {
                particles[i].remainingLifetime = 0.015f;
            }
        }

        pSystem.SetParticles(particles, aliveParticles);
    }
}
