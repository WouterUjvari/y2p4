using UnityEngine;

public class SafetyNet : MonoBehaviour 
{

    [SerializeField]
    private Transform savedItemsSpawn;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<Interactable>() != null)
        {
            Rigidbody rb = other.transform.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
            }

            other.transform.position = savedItemsSpawn.position;
        }
    }
}
