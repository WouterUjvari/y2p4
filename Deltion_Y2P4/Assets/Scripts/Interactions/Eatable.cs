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

	private VRInteractor interactingHand;

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
		Grab(hand);
        trackPosition = true;

		interactingHand = hand;
    }

    public override void DeInteract(VRInteractor hand)
    {
		Release(hand);
        trackPosition = false;

		interactingHand = null;
    }

    private void EatObject()
    {
        eatEvent.Invoke();

		for (int i = 0; i < collidersToTurnOff.Count; i++)
		{
			collidersToTurnOff[i].enabled = true;
		}

        if (eatParticle != null)
        {
            Instantiate(eatParticle, transform.position, Quaternion.identity);
        }

		interactingHand.DeInteract ();

        Destroy(gameObject);
    }
}
