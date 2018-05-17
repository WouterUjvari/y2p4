using UnityEngine;
using UnityEngine.Events;

public class Eatable : Grabable 
{

    private bool trackPosition;

    [SerializeField]
    private UnityEvent eatEvent;

    [SerializeField]
    private float eatDistance = 0.2f;

    [SerializeField]
    private GameObject eatParticle;

    private void Update()
    {
        if (!trackPosition)
        {
            return;
        }

        if (Vector3.Distance(transform.position, VRPlayerMovementManager.instance.headTransform.position) < eatDistance)
        {
            EatObject();
        }
    }

    public override void Interact(VRInteractor hand)
    {
        base.Interact(hand);

        trackPosition = true;
    }

    public override void DeInteract(VRInteractor hand)
    {
        base.DeInteract(hand);

        trackPosition = false;
    }

    private void EatObject()
    {
        eatEvent.Invoke();

        if (eatParticle != null)
        {
            Instantiate(eatParticle, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
