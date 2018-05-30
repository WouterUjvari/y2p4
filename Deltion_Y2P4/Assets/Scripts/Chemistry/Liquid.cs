using System.Collections.Generic;
using UnityEngine;

public class Liquid : MonoBehaviour
{

    private Flask myFlask;
    private ParticleSystem pSystem;
    private ParticleSystem.MainModule psModule;

    private void Awake()
    {
        myFlask = GetComponentInParent<Flask>();
        pSystem = GetComponent<ParticleSystem>();
        psModule = pSystem.main;
    }

    private void OnParticleCollision(GameObject other)
    {
        Flask flask = other.GetComponent<Flask>();
        if (flask != null && flask != myFlask)
        {
            flask.DestroyNearbyParticles(pSystem);
            flask.AddLiquid(psModule.startColor.color);
        }

        Colourable colourable = other.GetComponent<Colourable>();
        if (colourable != null)
        {
            colourable.ReColor(psModule.startColor.color);
        }
    }
}
