using UnityEngine;

public class FireExtinguisherCase : MonoBehaviour 
{

    [SerializeField]
    private Interactable fireExtinguisher;

    [Space(10)]

    [SerializeField]
    private GameObject glass;
    [SerializeField]
    private GameObject brokenGlass;
    [SerializeField]
    private float glassImpactForce = 1.5f;
    [SerializeField]
    private AudioSource glassBreakAudio;
    [SerializeField]
    private Collider hitCollider;

    private void Awake()
    {
        fireExtinguisher.Lock(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        VRInteractor hand = other.GetComponent<VRInteractor>();

        if (hand != null)
        {
            if (Mathf.Abs(hand.Controller.velocity.x) > 0.75f || Mathf.Abs(hand.Controller.velocity.z) > 0.75f)
            {
                BreakGlass(hand);
            }
        }
    }

    private void BreakGlass(VRInteractor hand)
    {
        hitCollider.enabled = false;

        glass.SetActive(false);
        brokenGlass.SetActive(true);
        for (int i = 0; i < brokenGlass.transform.childCount; i++)
        {
            brokenGlass.transform.GetChild(i).GetComponent<Rigidbody>().AddForce(-hand.Controller.velocity * glassImpactForce, ForceMode.Impulse);
        }

        if (glassBreakAudio.clip != null)
        {
            glassBreakAudio.Play();
        }

        fireExtinguisher.Lock(false);
    }
}
