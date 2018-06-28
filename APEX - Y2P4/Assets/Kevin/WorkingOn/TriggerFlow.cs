using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFlow : MonoBehaviour {

	private FlowManager fM;
	public bool canTrigger;
	public bool triggerFlow;
	public bool triggerAnouncer;
	public bool triggerShipAI;
	public float timeTillTrigger;
	void Start()
	{
		fM = FindObjectOfType<FlowManager>();
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			if(canTrigger)
			{
				canTrigger = false;
				StartCoroutine(TimerBeforeTrigger(timeTillTrigger));
			}
		}
	}

	public void Activate()
	{
		canTrigger = true;
	}

    public void DroneBuildEvent()
    {

    }

	public IEnumerator TimerBeforeTrigger(float time)
	{
		yield return new WaitForSeconds(time);
		if(triggerFlow)
		{
			fM.NextPuzzle(3);
		}
		if(triggerAnouncer)
		{
			fM.NextAnouncerVoice(3);
		}
		if(triggerShipAI)
		{
			fM.NextShipAIVoice(3);
		}
        this.gameObject.SetActive(false);
	}
}
