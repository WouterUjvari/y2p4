using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DebugInteraction : MonoBehaviour {

	public UnityEvent interactEvent;

	void Start () {
		interactEvent.Invoke();
	}
}
