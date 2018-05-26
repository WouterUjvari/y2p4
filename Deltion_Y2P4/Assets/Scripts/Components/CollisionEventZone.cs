using UnityEngine;
using UnityEngine.Events;

public class CollisionEventZone : MonoBehaviour 
{

    [SerializeField]
    private string lookForTag;

    [SerializeField]
    private UnityEvent collisionEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == lookForTag)
        {
            collisionEvent.Invoke();
        }
    }
}
