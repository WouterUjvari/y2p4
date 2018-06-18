using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DebugInteraction : MonoBehaviour {

	public UnityEvent interactEvent;
	public bool keepGoing;
	public float timer;

	void Start () 
	{
		StartCoroutine(ActivateTimer(0));
	}

	public void InvokeInteract()
	{
		interactEvent.Invoke();
		if(keepGoing == true)
		{
			StartCoroutine(ActivateTimer(timer));
		}
	}
	public IEnumerator ActivateTimer(float timer)
	{
		yield return new WaitForSeconds(timer);
		InvokeInteract();
	}
}
