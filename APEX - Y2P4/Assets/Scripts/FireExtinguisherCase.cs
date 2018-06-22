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
            if (Mathf.Abs(hand.Controller.velocity.x) > 5 || Mathf.Abs(hand.Controller.velocity.z) > 5)
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
            brokenGlass.transform.GetChild(i).GetComponent<Rigidbody>().AddForce(-hand.Controller.velocity, ForceMode.Impulse);
        }

        fireExtinguisher.Lock(false);
    }
}
