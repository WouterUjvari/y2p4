using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IRLMovementRestrictions : MonoBehaviour {

	public void OnTriggerEnter(Collider other)
	{
		if(other.transform.gameObject.layer == 14)
		{
			gameObject.GetComponent<Rigidbody>().velocity = Vector3.back * Time.deltaTime;
		}
	}
}
