using System.Collections.Generic;
using UnityEngine;

public class Liquid : MonoBehaviour 
{

    private Flask myFlask;
    private ParticleSystem.MainModule psModule;

    private void Awake()
    {
        myFlask = GetComponentInParent<Flask>();
        psModule = GetComponent<ParticleSystem>().main;
    }

    private void OnParticleCollision(GameObject other)
    {
        Flask flask = other.GetComponent<Flask>();
        if (flask != null && flask != myFlask)
        {
            flask.AddLiquid(psModule.startColor.color);
        }
    }
}
