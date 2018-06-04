using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flask : MonoBehaviour
{

    [SerializeField]
    private string myColorName;
    [SerializeField]
    private List<Colors> colors = new List<Colors>();

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

    [System.Serializable]
    private struct Colors
    {
        public Color color;
        public string name;
    }

    private void Awake()
    {
        for (int i = 0; i < colors.Count; i++)
        {
            if (string.IsNullOrEmpty(myColorName))
            {
                break;
            }

            if (colors[i].name == myColorName)
            {
                myCurrentColor = colors[i].color;
            }
        }

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
            //Color newColor = (myCurrentColor + color) / 2;
            Color newColor = (staticLiquid.localScale.z == 0) ? myCurrentColor : GetMixedColor(color);
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

        isLerpingColor = false;
    }

    private Color GetMixedColor(Color toMix)
    {
        Color mixedColor = new Color();
        string mixedColorName = null;

        string toMixName = null;

        for (int i = 0; i < colors.Count; i++)
        {
            if (colors[i].color == toMix)
            {
                toMixName = colors[i].name;
            }
        }

        switch (myColorName)
        {
            case "red":

                switch (toMixName)
                {
                    case "red":

                        mixedColorName = "red";
                        break;

                    case "blue":

                        mixedColorName = "purple";
                        break;
                    case "yellow":

                        mixedColorName = "purple";
                        break;
                    case "green":

                        mixedColorName = "brown";
                        break;
                    case "purple":

                        mixedColorName = "yellow";
                        break;
                    case "brown":

                        mixedColorName = "blue";
                        break;
                }
                break;
            case "blue":

                switch (toMixName)
                {
                    case "red":

                        mixedColorName = "purple";
                        break;

                    case "blue":

                        mixedColorName = "blue";
                        break;
                    case "yellow":

                        mixedColorName = "brown";
                        break;
                    case "green":

                        mixedColorName = "yellow";
                        break;
                    case "purple":

                        mixedColorName = "red";
                        break;
                    case "brown":

                        mixedColorName = "purple";
                        break;
                }
                break;
            case "yellow":

                switch (toMixName)
                {
                    case "red":

                        mixedColorName = "purple";
                        break;

                    case "blue":

                        mixedColorName = "purple";
                        break;
                    case "yellow":

                        mixedColorName = "yellow";
                        break;
                    case "green":

                        mixedColorName = "blue";
                        break;
                    case "purple":

                        mixedColorName = "green";
                        break;
                    case "brown":

                        mixedColorName = "green";
                        break;
                }
                break;
            case "green":

                switch (toMixName)
                {
                    case "red":

                        mixedColorName = "purple";
                        break;

                    case "blue":

                        mixedColorName = "brown";
                        break;
                    case "yellow":

                        mixedColorName = "red";
                        break;
                    case "green":

                        mixedColorName = "green";
                        break;
                    case "purple":

                        mixedColorName = "blue";
                        break;
                    case "brown":

                        mixedColorName = "red";
                        break;
                }
                break;
            case "purple":

                switch (toMixName)
                {
                    case "red":

                        mixedColorName = "yellow";
                        break;

                    case "blue":

                        mixedColorName = "red";
                        break;
                    case "yellow":

                        mixedColorName = "blue";
                        break;
                    case "green":

                        mixedColorName = "brown";
                        break;
                    case "purple":

                        mixedColorName = "purple";
                        break;
                    case "brown":

                        mixedColorName = "yellow";
                        break;
                }
                break;
            case "brown":

                switch (toMixName)
                {
                    case "red":

                        mixedColorName = "blue";
                        break;

                    case "blue":

                        mixedColorName = "purple";
                        break;
                    case "yellow":

                        mixedColorName = "red";
                        break;
                    case "green":

                        mixedColorName = "purple";
                        break;
                    case "purple":

                        mixedColorName = "yellow";
                        break;
                    case "brown":

                        mixedColorName = "brown";
                        break;
                }
                break;
        }

        for (int i = 0; i < colors.Count; i++)
        {
            if (colors[i].name == mixedColorName)
            {
                mixedColor = colors[i].color;
            }
        }

        return mixedColor;
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
