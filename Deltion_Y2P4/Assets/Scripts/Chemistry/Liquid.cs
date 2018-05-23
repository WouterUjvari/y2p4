using UnityEngine;

public class Liquid : MonoBehaviour 
{

    private ParticleSystem.MainModule psModule;

    private void Awake()
    {
        psModule = GetComponent<ParticleSystem>().main;
    }

    private void OnParticleCollision(GameObject other)
    {
        Flask flask = other.GetComponent<Flask>();
        if (flask != null)
        {
            flask.AddLiquid(psModule.startColor.color);
        }
    }
}
