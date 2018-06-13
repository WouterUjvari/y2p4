using UnityEngine;

[RequireComponent(typeof(Light))]
public class RevealingLight : MonoBehaviour 
{

    private Light light;

    [SerializeField]
    private Material matToReveal;

    private void Awake()
    {
        light = GetComponent<Light>();
    }

    private void Update()
    {
        matToReveal.SetVector("_LightPosition", light.transform.position);
        matToReveal.SetVector("_LightDirection", -light.transform.forward);
        matToReveal.SetFloat("_LightAngle", light.spotAngle);
    }
}
