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
    [SerializeField]
    private ObjectSnapSpot toBurnSnapSpot;
    [SerializeField]
    private ParticleSystem burnedLiquidParticle;
    [SerializeField]
    private Animator springTubeAnim;
    [SerializeField]
    private Material springTubeLiquidMat;
    [SerializeField]
    private float burningLiquidEmptySpeed = 0.1f;
    [SerializeField]
    private Liquid springTubeLiquid;

    private float flameStrength = 0.3f;
    private bool changingFlameStrength;
    private ParticleSystem.MainModule flameParticleMain;
    private ParticleSystem.MainModule burnedLiquidParticleMain;

    private void Awake()
    {
        flameParticleMain = flameParticle.main;
        flameParticleMain.startLifetime = flameStrength;

        burnedLiquidParticleMain = burnedLiquidParticle.main;
    }

    private void Update()
    {
        if (changingFlameStrength)
        {
            float switchZ = flameStrengthSwitch.localEulerAngles.z > 180 ? flameStrengthSwitch.localEulerAngles.z - 360 : flameStrengthSwitch.localEulerAngles.z;
            flameStrength = switchZ.Remap(flameStrengthSwitchRotator.maxRot, flameStrengthSwitchRotator.minRot, 0.3f, 1);
            flameParticleMain.startLifetime = flameStrength;

            //flameParticle.gameObject.SetActive(flameStrength < 0.05 ? false : true);
        }
    }

    public void ChangeFlameStrength(bool b)
    {
        changingFlameStrength = b;
    }

    public void StartBurning()
    {
        Flask toBurn = toBurnSnapSpot.snappedObject.GetComponentInChildren<Flask>();
        if (!toBurn.isEmpty)
        {
            StartCoroutine(BurnLiquid(toBurn));
        }
    }

    private IEnumerator BurnLiquid(Flask toBurn)
    {
        yield return new WaitForSeconds(1);

        Color burnedColor = ColorMixingManager.instance.GetBurnedColor(toBurn.myColorName);

        StartCoroutine(toBurn.EmptyFlask(burningLiquidEmptySpeed));
        burnedLiquidParticleMain.startColor = burnedColor;
        springTubeLiquidMat.color = burnedColor;
        springTubeLiquid.myColor = burnedColor;
        springTubeAnim.SetTrigger("Go");

        yield return new WaitForSeconds(5.3f);

        burnedLiquidParticle.Play();
        yield return new WaitForSeconds(3.9f);
        burnedLiquidParticle.Stop();

        toBurnSnapSpot.snappedObject.GetComponent<Interactable>().Lock(false);
    }
}
