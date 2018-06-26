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
        glass.SetActive(false);
        brokenGlass.SetActive(true);
        for (int i = 0; i < brokenGlass.transform.childCount; i++)
        {
            brokenGlass.transform.GetChild(i).GetComponent<Rigidbody>().AddForce(-hand.Controller.velocity * 0.5f, ForceMode.Impulse);
        }

        fireExtinguisher.GetComponent<Rigidbody>().isKinematic = false;
        fireExtinguisher.Lock(false);
    }
}
