using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burner : MonoBehaviour 
{

    [SerializeField]
    private ParticleSystem flameParticle;
    [SerializeField]
    private Transform flameStrengthSwitch;
    [SerializeField]
    private Rotator flameStrengthSwitchRotator;

    private float flameStrength;
    private bool changingFlameStrength;
    private ParticleSystem.MainModule flameParticleMain;

    private void Awake()
    {
        flameParticleMain = flameParticle.main;
        flameParticleMain.startLifetime = flameStrength;
    }

    private void Update()
    {
        if (changingFlameStrength)
        {
            float switchZ = flameStrengthSwitch.localEulerAngles.z > 180 ? flameStrengthSwitch.localEulerAngles.z - 360 : flameStrengthSwitch.localEulerAngles.z;
            flameStrength = switchZ.Remap(flameStrengthSwitchRotator.maxRot, flameStrengthSwitchRotator.minRot, 0, 1);
            flameParticleMain.startLifetime = flameStrength;

            flameParticle.gameObject.SetActive(flameStrength < 0.05 ? false : true);
        }
    }

    public void ChangeFlameStrength(bool b)
    {
        changingFlameStrength = b;
    }
}
